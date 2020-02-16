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

    public class BookCopiesController : Controller {

        private readonly LibraryContext _context;

        public BookCopiesController(LibraryContext context) {
            _context = context;
        }

        private IQueryable<Borrowing> GetBorrowHistory(int id) {
            var orderBy = HttpContext.Request.Query["OrderBy"];
            ViewData["OrderBy"] = orderBy;
            ViewData["LoginSortParam"] = orderBy == "Login" ? "Login_desc" : "Login";
            ViewData["NameSortParam"] = orderBy == "Name" ? "Name_desc" : "Name";
            ViewData["BorrowDateSortParam"] = orderBy == "BorrowDate_desc" ? "BorrowDate" : "BorrowDate_desc";
            ViewData["ReturnDateSortParam"] = string.IsNullOrEmpty(orderBy) ? "ReturnDate" : "";
            IQueryable<Borrowing> borrowings = _context.Borrowing
                .Where(i => i.CopyId == id)
                .Include(i => i.UserLoginNavigation)
                    .ThenInclude(r => r.LoginNavigation);
            switch (orderBy) {
                case "Login_desc":
                    borrowings = borrowings.OrderByDescending(i => i.UserLogin);
                    break;
                case "Login":
                    borrowings = borrowings.OrderBy(i => i.UserLogin);
                    break;
                case "Name_desc":
                    borrowings = borrowings
                        .OrderByDescending(i => i.UserLoginNavigation.LoginNavigation.LastName)
                            .ThenByDescending(i => i.UserLoginNavigation.LoginNavigation.FirstName);
                    break;
                case "Name":
                    borrowings = borrowings
                        .OrderBy(i => i.UserLoginNavigation.LoginNavigation.LastName)
                            .ThenBy(i => i.UserLoginNavigation.LoginNavigation.FirstName);
                    break;
                case "BorrowDate_desc":
                    borrowings = borrowings.OrderByDescending(i => i.BorrowDate);
                    break;
                case "BorrowDate":
                    borrowings = borrowings.OrderBy(i => i.BorrowDate);
                    break;
                case "ReturnDate":
                    borrowings = borrowings
                        .OrderByDescending(i => i.ReturnDate.HasValue)
                            .ThenBy(i => i.ReturnDate);
                    break;
                default:
                    borrowings = borrowings
                        .OrderBy(i => i.ReturnDate.HasValue)
                            .ThenByDescending(i => i.ReturnDate);
                    break;
            }
            return borrowings;
        }

        // GET: BookCopies
        public IActionResult Index() {
            return RedirectToAction(nameof(BooksController.Index), "Books");
        }

        // GET: BookCopies/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var bookCopy = await _context.BookCopy
                .Include(b => b.BranchNumberNavigation)
                .Include(b => b.Edition)
                    .ThenInclude(e => e.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookCopy == null) {
                return NotFound();
            }

            var viewModel = new BookCopiesViewModel {
                Copy = bookCopy,
                Borrowings = await GetBorrowHistory(bookCopy.Id).ToListAsync()
            };
            var state = (await _context.BookCopyState.FirstOrDefaultAsync(i => i.Id == id)).State;
            if (state == "borrowed") {
                var borrowing = await _context.Borrowing
                    .Include(i => i.UserLoginNavigation)
                        .ThenInclude(i => i.LoginNavigation)
                    .FirstOrDefaultAsync(i => i.CopyId == id && i.ReturnDate == null);
                viewModel.Reader = borrowing.UserLoginNavigation;
                ViewData["BorrowDate"] = borrowing.BorrowDate;
            }
            if (state == "reserved") {
                viewModel.Reader = (await _context.Reservation
                    .Include(i => i.UserLoginNavigation)
                        .ThenInclude(i => i.LoginNavigation)
                    .FirstOrDefaultAsync(i => i.CopyId == id))
                    .UserLoginNavigation;
            }

            ViewData["Authors"] = await _context.BookAuthors(bookCopy.Edition.BookId).ToListAsync();
            ViewData["State"] = state;
            return View(viewModel);
        }

        // POST: BookCopies/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int? id, [Bind("Copy,ReaderQuery")]BookCopiesViewModel viewModel) {
            if (id == null) {
                return NotFound();
            }

            var bookCopy = await _context.BookCopy
                .Include(b => b.BranchNumberNavigation)
                .Include(b => b.Edition)
                    .ThenInclude(e => e.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookCopy == null) {
                return NotFound();
            }

            viewModel.Copy = bookCopy;
            viewModel.Borrowings = await GetBorrowHistory(bookCopy.Id).ToListAsync();
            var state = (await _context.BookCopyState.FirstOrDefaultAsync(i => i.Id == id)).State;
            if (state == "borrowed") {
                var borrowing = await _context.Borrowing
                    .Include(i => i.UserLoginNavigation)
                        .ThenInclude(i => i.LoginNavigation)
                    .FirstOrDefaultAsync(i => i.CopyId == id && i.ReturnDate == null);
                viewModel.Reader = borrowing.UserLoginNavigation;
                ViewData["BorrowDate"] = borrowing.BorrowDate;
            }
            else if (state == "reserved") {
                viewModel.Reader = (await _context.Reservation
                    .Include(i => i.UserLoginNavigation)
                        .ThenInclude(i => i.LoginNavigation)
                    .FirstOrDefaultAsync(i => i.CopyId == id))
                    .UserLoginNavigation;
            }
            else {
                viewModel.Reader = await _context.Reader
                    .Include(i => i.LoginNavigation)
                    .FirstOrDefaultAsync(i => i.Login == viewModel.ReaderQuery);
                if (viewModel.Reader == null) {
                    ViewData["errMsg"] = "Nie znaleziono czytelnika o podanym loginie";
                }
            }

            ViewData["Authors"] = await _context.BookAuthors(bookCopy.Edition.BookId).ToListAsync();
            ViewData["State"] = state;
            return View(viewModel);
        }

        // GET: BookCopies/Create
        public IActionResult Create(int? id) {
            var book = _context.Book.Find(id);
            if (id == null || book == null) {
                return NotFound();
            }

            var editions = _context.Edition
                .Where(i => i.BookId == id)
                .OrderByDescending(i => i.ReleaseDate);
            ViewData["BranchNumber"] = new SelectList(_context.Branch, "BranchNumber", "Name");
            ViewData["EditionId"] = new SelectList(editions, "Id", "EditionString");
            ViewData["Title"] = book.Title;
            return View();
        }

        // POST: BookCopies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,
            [Bind("EditionId,BranchNumber,Condition")] BookCopy bookCopy) {
            if (ModelState.IsValid) {
                _context.Add(bookCopy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var book = _context.Book.Find(id);
            if (book == null) {
                return NotFound();
            }
            var editions = _context.Edition
                .Where(i => i.BookId == id)
                .OrderByDescending(i => i.ReleaseDate);
            ViewData["BranchNumber"] = new SelectList(_context.Branch, "BranchNumber", "Name", bookCopy.BranchNumber);
            ViewData["EditionId"] = new SelectList(editions, "Id", "EditionString", bookCopy.EditionId);
            ViewData["Title"] = book.Title;
            return View(bookCopy);
        }

        // GET: BookCopies/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var bookCopy = await _context.BookCopy
                .Include(i => i.Edition)
                    .ThenInclude(i => i.Book)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (bookCopy == null) {
                return NotFound();
            }
            var editions = _context.Edition
                .Where(i => i.BookId == bookCopy.Edition.BookId)
                .OrderByDescending(i => i.ReleaseDate);
            ViewData["BranchNumber"] = new SelectList(_context.Branch, "BranchNumber", "Name", bookCopy.BranchNumber);
            ViewData["EditionId"] = new SelectList(editions, "Id", "EditionString", bookCopy.EditionId);
            return View(bookCopy);
        }

        // POST: BookCopies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Edition,EditionId,BranchNumber,Condition")] BookCopy bookCopy) {
            if (id != bookCopy.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(bookCopy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!BookCopyExists(bookCopy.Id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var editions = _context.Edition
                .Where(i => i.BookId == bookCopy.Edition.BookId)
                .OrderByDescending(i => i.ReleaseDate);
            ViewData["BranchNumber"] = new SelectList(_context.Branch, "BranchNumber", "Name", bookCopy.BranchNumber);
            ViewData["EditionId"] = new SelectList(editions, "Id", "EditionString", bookCopy.EditionId);
            return View(bookCopy);
        }

        // GET: BookCopies/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var bookCopy = await _context.BookCopy
                .Include(b => b.BranchNumberNavigation)
                .Include(b => b.Edition)
                    .ThenInclude(e => e.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookCopy == null) {
                return NotFound();
            }
            ViewData["Authors"] = await _context.BookAuthors(bookCopy.Edition.BookId).ToListAsync();
            return View(bookCopy);
        }

        // POST: BookCopies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var bookCopy = await _context.BookCopy.FindAsync(id);
            _context.BookCopy.Remove(bookCopy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookCopyExists(int id) {
            return _context.BookCopy.Any(e => e.Id == id);
        }
    }
}
