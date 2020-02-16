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

    public class LibrariansController : Controller {

        private readonly LibraryContext _context;

        public LibrariansController(LibraryContext context) {
            _context = context;
        }

        // GET: Librarians
        public IActionResult Index() {
            return RedirectToAction(nameof(BranchesController.Index), "Branches");
        }

        // GET: Librarians/Create
        public IActionResult Create() {
            ViewData["BranchNumber"] = new SelectList(_context.Branch, "BranchNumber", "Name");
            return View();
        }

        // POST: Librarians/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Login,LoginNavigation,EmploymentDate,BranchNumber")] Librarian librarian) {
            if (ModelState.IsValid) {
                _context.Add(librarian.LoginNavigation);
                _context.Add(librarian);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchNumber"] = new SelectList(_context.Branch, "BranchNumber", "Name", librarian.BranchNumber);
            return View(librarian);
        }

        // GET: Librarians/Edit/5
        public async Task<IActionResult> Edit(string id) {
            if (id == null) {
                return NotFound();
            }

            var librarian = await _context.Librarian
                .Include(i => i.LoginNavigation)
                .FirstOrDefaultAsync(i => i.Login == id);
            if (librarian == null) {
                return NotFound();
            }
            ViewData["BranchNumber"] = new SelectList(_context.Branch, "BranchNumber", "Name", librarian.BranchNumber);
            return View(librarian);
        }

        // POST: Librarians/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Login,LoginNavigation,EmploymentDate,BranchNumber")] Librarian librarian) {
            if (id != librarian.Login) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(librarian.LoginNavigation);
                    _context.Update(librarian);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!LibrarianExists(librarian.Login)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchNumber"] = new SelectList(_context.Branch, "BranchNumber", "Name", librarian.BranchNumber);
            return View(librarian);
        }

        // GET: Librarians/Delete/5
        public async Task<IActionResult> Delete(string id) {
            if (id == null) {
                return NotFound();
            }

            var librarian = await _context.Librarian
                .Include(l => l.BranchNumberNavigation)
                .Include(l => l.LoginNavigation)
                .FirstOrDefaultAsync(m => m.Login == id);
            if (librarian == null) {
                return NotFound();
            }

            return View(librarian);
        }

        // POST: Librarians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id) {
            var librarian = await _context.User.FindAsync(id);
            _context.User.Remove(librarian);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibrarianExists(string id) {
            return _context.Librarian.Any(e => e.Login == id);
        }
    }
}
