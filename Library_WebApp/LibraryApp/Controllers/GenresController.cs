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

    public class GenresController : Controller {

        private readonly LibraryContext _context;

        public GenresController(LibraryContext context) {
            _context = context;
        }

        // GET: Genres
        public async Task<IActionResult> Index() {
            // Sorting
            var orderBy = HttpContext.Request.Query["OrderBy"];
            ViewData["OrderBy"] = orderBy;
            ViewData["NameSortParam"] = "";
            if (string.IsNullOrEmpty(orderBy)) {
                orderBy = "Name";
                ViewData["NameSortParam"] = "Name_desc";
            }
            IEnumerable<Genre> genres;
            try {
                genres = await _context.Genre
                    .OrderByQuery(orderBy)
                    .ToListAsync();
            }
            catch (InvalidOperationException) {
                genres = await _context.Genre
                    .OrderBy(i => i.Name)
                    .ToListAsync();
            }

            return View(genres);
        }

        // GET: Genres/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Genres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Genre genre) {
            if (ModelState.IsValid) {
                _context.Add(genre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(string id) {
            if (id == null) {
                return NotFound();
            }

            var genre = await _context.Genre.FindAsync(id);
            if (genre == null) {
                return NotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name")] Genre genre) {
            if (ModelState.IsValid) {
                await _context.Database.ExecuteSqlInterpolatedAsync
                    ($"UPDATE Genre SET name={genre.Name} WHERE name={id}");
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(string id) {
            if (id == null) {
                return NotFound();
            }

            var genre = await _context.Genre
                .FirstOrDefaultAsync(m => m.Name == id);
            if (genre == null) {
                return NotFound();
            }

            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id) {
            var genre = await _context.Genre.FindAsync(id);
            _context.Genre.Remove(genre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(string id) {
            return _context.Genre.Any(e => e.Name == id);
        }
    }
}
