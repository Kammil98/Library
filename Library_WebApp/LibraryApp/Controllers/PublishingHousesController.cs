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
            var viewModel = new PublishingHousesViewModel {
                PublishingHouses = await _context.PublishingHouse
                .Include(i => i.Address)
                .OrderBy(i => i.Name)
                .ToListAsync()
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
                    _context.Address.Where(i => i.Id == viewModel.Selection.AddressId).Load();

                    viewModel.Editions = await _context.Edition
                        .Where(i => i.PublishingHouse == id)
                        .Include(i => i.Book)
                        .ToListAsync();
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
