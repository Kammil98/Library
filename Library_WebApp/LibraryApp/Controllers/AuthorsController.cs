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

    public class AuthorsController : Controller {

        private readonly LibraryContext _context;

        public AuthorsController(LibraryContext context) {
            _context = context;
        }

        // GET: Authors
        public async Task<IActionResult> Index(int? id = null) {
            // Sorting
            var orderBy = HttpContext.Request.Query["OrderBy"];
            ViewData["OrderBy"] = orderBy;
            ViewData["DetailOrderBy"] = "";
            ViewData["LastNameSortParam"] = orderBy == "LastName" ? "LastName_desc" : "LastName";
            ViewData["FirstNameSortParam"] = "";
            if (string.IsNullOrEmpty(orderBy)) {
                orderBy = "FirstName";
                ViewData["FirstNameSortParam"] = "FirstName_desc";
            }
            AuthorsViewModel viewModel;
            try {
                viewModel = new AuthorsViewModel {
                    Authors = await _context.Author
                        .OrderByQuery(orderBy)
                        .ToListAsync()
                };
            }
            catch (InvalidOperationException) {
                viewModel = new AuthorsViewModel {
                    Authors = await _context.Author
                        .OrderBy(i => i.FirstName)
                        .ToListAsync()
                };
            }
            // Filtering
            var nameFilter = HttpContext.Request.Query["NameFilter"];
            viewModel.NameFilter = nameFilter;
            var countryFilter = HttpContext.Request.Query["CountryFilter"];
            viewModel.CountryFilter = countryFilter;
            var name = nameFilter.ToString()
                .ToLower()
                .Split(new char[0], StringSplitOptions.RemoveEmptyEntries)
                .ToHashSet();
            viewModel.Authors = viewModel.Authors
                .Where(i => {
                    if (i.Country == null || !i.Country.ToLower().StartsWith(countryFilter.ToString().ToLower())) {
                        return false;
                    }
                    if (string.IsNullOrEmpty(nameFilter)) {
                        return true;
                    }
                    var match = string.Join(' ', i.FirstName, i.LastName)
                        .ToLower()
                        .Split(new char[0], StringSplitOptions.RemoveEmptyEntries)
                        .ToHashSet();
                    return name.IsSubsetOf(match);
                });
            // Detail view
            if (id != null) {
                viewModel.Selection = viewModel.Authors
                    .FirstOrDefault(i => i.Id == id);
                if (viewModel.Selection != null) {
                    var detailOrderBy = HttpContext.Request.Query["DetailOrderBy"];
                    ViewData["DetailOrderBy"] = detailOrderBy;
                    ViewData["GenreSortParam"] = detailOrderBy == "Genre" ? "Genre_desc" : "Genre";
                    ViewData["TitleSortParam"] = "";
                    if (string.IsNullOrEmpty(detailOrderBy)) {
                        detailOrderBy = "Title";
                        ViewData["TitleSortParam"] = "Title_desc";
                    }
                    try {
                        viewModel.Books = await _context.Book
                            .Include(i => i.Authorship)
                            .Where(i => i.Authorship.Any(a => a.AuthorId == id))
                            .OrderByQuery(detailOrderBy)
                            .ToListAsync();
                    }
                    catch (InvalidOperationException) {
                        viewModel.Books = await _context.Book
                            .Include(i => i.Authorship)
                            .Where(i => i.Authorship.Any(a => a.AuthorId == id))
                            .OrderBy(i => i.Title)
                            .ToListAsync();
                    }
                }
            }

            return View(viewModel);
        }

        // GET: Authors/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Country")] Author author) {
            if (ModelState.IsValid) {
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var author = await _context.Author.FindAsync(id);
            if (author == null) {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Country")] Author author) {
            if (id != author.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!AuthorExists(author.Id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var author = await _context.Author
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null) {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var author = await _context.Author.FindAsync(id);
            _context.Author.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id) {
            return _context.Author.Any(e => e.Id == id);
        }
    }
}
