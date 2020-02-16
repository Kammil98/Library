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

    public class EditionsController : Controller {

        private readonly LibraryContext _context;

        public EditionsController(LibraryContext context) {
            _context = context;
        }

        // GET: Editions
        public IActionResult Index() {
            return RedirectToAction(nameof(PublishingHousesController.Index), "PublishingHouses");
        }

        // GET: Editions/Create
        public IActionResult Create(int? id) {
            var book = _context.Book.Find(id);
            if (id == null || book == null) {
                return NotFound();
            }
            ViewData["Title"] = book.Title;
            return View();
        }

        // POST: Editions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,ReleaseDate,PublishingHouse")] Edition edition) {
            if (ModelState.IsValid) {
                _context.Add(edition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(BooksController.Index), "Books");
            }
            var book = _context.Book.Find(edition.BookId);
            if (book == null) {
                return NotFound();
            }
            ViewData["Title"] = book.Title;
            return View(edition);
        }

        // GET: Editions/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var edition = await _context.Edition
                .Include(e => e.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (edition == null) {
                return NotFound();
            }
            return View(edition);
        }

        // POST: Editions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,ReleaseDate,PublishingHouse")] Edition edition) {
            if (id != edition.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(edition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!EditionExists(edition.Id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(edition);
        }

        // GET: Editions/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var edition = await _context.Edition
                .Include(e => e.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (edition == null) {
                return NotFound();
            }
            ViewData["Authors"] = await _context.BookAuthors(edition.BookId).ToListAsync();
            return View(edition);
        }

        // POST: Editions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var edition = await _context.Edition.FindAsync(id);
            _context.Edition.Remove(edition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EditionExists(int id) {
            return _context.Edition.Any(e => e.Id == id);
        }
    }
}
