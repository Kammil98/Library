using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryApp;
using LibraryApp.Models;

namespace LibraryApp.Controllers {

    public class PublishingHousesController : Controller {

        private readonly LibraryContext _context;

        public PublishingHousesController(LibraryContext context) {
            _context = context;
        }

        // GET: PublishingHouses
        public async Task<IActionResult> Index(string id = null) {
            var orderBy = HttpContext.Request.Query["OrderBy"];
            ViewData["OrderBy"] = orderBy;
            ViewData["DetailOrderBy"] = "";
            ViewData["NameSortParam"] = string.IsNullOrEmpty(orderBy) ? "name_desc" : "";
            IQueryable<PublishingHouse> publishingHouses = _context.PublishingHouse
                .Include(i => i.Address);
            switch (orderBy) {
                case "name_desc":
                    publishingHouses = publishingHouses.OrderByDescending(i => i.Name);
                    break;
                default:
                    publishingHouses = publishingHouses.OrderBy(i => i.Name);
                    break;
            }
            var viewModel = new PublishingHousesViewModel {
                PublishingHouses = await publishingHouses.ToListAsync()
            };

            var nameFilter = HttpContext.Request.Query["NameFilter"];
            viewModel.NameFilter = nameFilter;
            var addressFilter = HttpContext.Request.Query["AddressFilter"];
            viewModel.AddressFilter = addressFilter;

            viewModel.PublishingHouses = viewModel.PublishingHouses
                .Where(i => {
                    if (!i.Name.ToLower().StartsWith(nameFilter.ToString().ToLower())) {
                        return false;
                    }
                    if (string.IsNullOrEmpty(addressFilter)) {
                        return true;
                    }
                    var address = addressFilter.ToString()
                        .ToLower()
                        .Split(new char[0], StringSplitOptions.RemoveEmptyEntries)
                        .ToHashSet();
                    var match = string.Join(' ',
                        i.Address.Street,
                        i.Address.Country,
                        i.Address.City,
                        i.Address.ZipCode)
                        .ToLower()
                        .Split(new char[0], StringSplitOptions.RemoveEmptyEntries)
                        .ToHashSet();
                    return address.IsSubsetOf(match);
                });

            if (id != null) {
                viewModel.Selection = viewModel.PublishingHouses
                    .FirstOrDefault(i => i.Name == id);
                if (viewModel.Selection != null) {
                    var detailOrderBy = HttpContext.Request.Query["DetailOrderBy"];
                    ViewData["DetailOrderBy"] = detailOrderBy;
                    ViewData["TitleSortParam"] = string.IsNullOrEmpty(detailOrderBy) ? "title_desc" : "";
                    ViewData["DateSortParam"] = detailOrderBy == "date" ? "date_desc" : "date";
                    IQueryable<Edition> editions = _context.Edition
                        .Where(i => i.PublishingHouse == id)
                        .Include(i => i.Book);
                    switch (detailOrderBy) {
                        case "date_desc":
                            editions = editions.OrderByDescending(i => i.ReleaseDate);
                            break;
                        case "date":
                            editions = editions.OrderBy(i => i.ReleaseDate);
                            break;
                        case "title_desc":
                            editions = editions.OrderByDescending(i => i.Book.Title);
                            break;
                        default:
                            editions = editions.OrderBy(i => i.Book.Title);
                            break;
                    }
                    viewModel.Editions = await editions.ToListAsync();
                }
            }

            return View(viewModel);
        }

        // GET: PublishingHouses/Create
        public IActionResult Create() {
            return View();
        }

        // POST: PublishingHouses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address")] PublishingHouse publishingHouse) {
            if (ModelState.IsValid) {
                var address = _context.Add(publishingHouse.Address);
                publishingHouse.AddressId = address.Entity.Id;
                _context.Add(publishingHouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publishingHouse);
        }

        // GET: PublishingHouses/Edit/5
        public async Task<IActionResult> Edit(string id) {
            if (id == null) {
                return NotFound();
            }

            var publishingHouse = await _context.PublishingHouse
                .Include(p => p.Address)
                .FirstOrDefaultAsync(m => m.Name == id);
            if (publishingHouse == null) {
                return NotFound();
            }
            return View(publishingHouse);
        }

        // POST: PublishingHouses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,AddressId,Address")] PublishingHouse publishingHouse) {
            if (id != publishingHouse.Name) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    publishingHouse.Address.Id = publishingHouse.AddressId;
                    _context.Update(publishingHouse.Address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!PublishingHouseExists(publishingHouse.Name)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(publishingHouse);
        }

        // GET: PublishingHouses/Delete/5
        public async Task<IActionResult> Delete(string id) {
            if (id == null) {
                return NotFound();
            }

            var publishingHouse = await _context.PublishingHouse
                .Include(p => p.Address)
                .FirstOrDefaultAsync(m => m.Name == id);
            if (publishingHouse == null) {
                return NotFound();
            }

            return View(publishingHouse);
        }

        // POST: PublishingHouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id) {
            var publishingHouse = await _context.PublishingHouse
                .Include(p => p.Address)
                .FirstOrDefaultAsync(m => m.Name == id);
            _context.PublishingHouse.Remove(publishingHouse);
            _context.Address.Remove(publishingHouse.Address);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublishingHouseExists(string id) {
            return _context.PublishingHouse.Any(e => e.Name == id);
        }
    }
}
