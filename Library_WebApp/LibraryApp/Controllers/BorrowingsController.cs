using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryApp;
using LibraryApp.Models;
using Microsoft.Data.SqlClient;

namespace LibraryApp.Controllers {

    public class BorrowingsController : Controller {

        private readonly LibraryContext _context;

        public BorrowingsController(LibraryContext context) {
            _context = context;
        }

        // GET: Borrowings
        public IActionResult Index(int? id) {
            return RedirectToAction(nameof(BookCopiesController.Details), "BookCopies", new { id });
        }

        // POST: Borrowings/Return
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int copyId) {
            await _context.ReturnCopy(copyId);
            return RedirectToAction(nameof(Index), new { id = copyId });
        }

        // POST: Borrowings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int copyId, string login) {
            await _context.BorrowCopy(copyId, login);
            return RedirectToAction(nameof(Index), new { id = copyId });
        }

        // GET: Borrowings/Edit/5
        public async Task<IActionResult> Edit(long? id) {
            var login = HttpContext.Request.Query["Login"].ToString();
            var copy = HttpContext.Request.Query["CopyId"].ToString();
            int.TryParse(copy, out int copyId);
            if (id == null || login == null || copyId == 0) {
                return NotFound();
            }

            var borrowDate = DateTimeOffset.FromUnixTimeMilliseconds(id.Value);
            var borrowing = await _context.Borrowing
                .Include(b => b.Copy)
                    .ThenInclude(c => c.Edition)
                        .ThenInclude(e => e.Book)
                .Include(b => b.UserLoginNavigation)
                    .ThenInclude(r => r.LoginNavigation)
                .FirstOrDefaultAsync(e => e.CopyId == copyId && e.UserLogin == login && e.BorrowDate == borrowDate);
            if (borrowing == null) {
                return NotFound();
            }
            return View(borrowing);
        }

        // POST: Borrowings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("UserLogin,CopyId,BorrowDate,ReturnDate")] Borrowing borrowing) {
            var borrowDate = DateTimeOffset.FromUnixTimeMilliseconds(id);
            if (ModelState.IsValid) {
                using var transaction = _context.Database.BeginTransaction();
                try {
                    var result = await _context.Database.ExecuteSqlInterpolatedAsync
                        ($"UPDATE Borrowing SET borrowDate={borrowing.BorrowDate} WHERE borrowDate={borrowDate} AND userLogin={borrowing.UserLogin} AND copyId={borrowing.CopyId}");
                    if (result > 0) {
                        _context.Update(borrowing);
                        await _context.SaveChangesAsync();
                        transaction.Commit();
                        return RedirectToAction(nameof(Index), new { id = borrowing.CopyId });
                    }
                }
                catch (DbUpdateConcurrencyException) {
                    if (!BorrowingExists(borrowing.CopyId, borrowing.UserLogin, borrowing.BorrowDate)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                catch (DbUpdateException) {
                    ViewData["errMsg"] = "Podano nieprawidłowe daty";
                }
                catch (SqlException) {
                    ViewData["errMsg"] = "Podano nieprawidłowe daty";
                }
            }
            borrowing = await _context.Borrowing
                .Include(b => b.Copy)
                    .ThenInclude(c => c.Edition)
                        .ThenInclude(e => e.Book)
                .Include(b => b.UserLoginNavigation)
                    .ThenInclude(r => r.LoginNavigation)
                .FirstOrDefaultAsync(e => e.CopyId == borrowing.CopyId
                    && e.UserLogin == borrowing.UserLogin
                    && e.BorrowDate == borrowDate);
            return View(borrowing);
        }

        // GET: Borrowings/Delete/5
        public async Task<IActionResult> Delete(long? id) {
            var login = HttpContext.Request.Query["Login"].ToString();
            var copy = HttpContext.Request.Query["CopyId"].ToString();
            int.TryParse(copy, out int copyId);
            if (id == null || login == null || copyId == 0) {
                return NotFound();
            }

            var borrowDate = DateTimeOffset.FromUnixTimeMilliseconds(id.Value);
            var borrowing = await _context.Borrowing
                .Include(b => b.Copy)
                    .ThenInclude(c => c.Edition)
                        .ThenInclude(e => e.Book)
                .Include(b => b.UserLoginNavigation)
                    .ThenInclude(r => r.LoginNavigation)
                .FirstOrDefaultAsync(e => e.CopyId == copyId && e.UserLogin == login && e.BorrowDate == borrowDate);
            if (borrowing == null) {
                return NotFound();
            }

            return View(borrowing);
        }

        // POST: Borrowings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id,
            [Bind("UserLogin,CopyId,BorrowDate")] Borrowing borrowing) {
            borrowing.BorrowDate = DateTimeOffset.FromUnixTimeMilliseconds(id);
            _context.Remove(borrowing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = borrowing.CopyId });
        }

        private bool BorrowingExists(int copyId, string login, DateTimeOffset borrowDate) {
            return _context.Borrowing.Any(e => e.CopyId == copyId && e.UserLogin == login && e.BorrowDate == borrowDate);
        }
    }
}
