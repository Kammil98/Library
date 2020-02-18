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

    public class ReadersController : Controller {

        private readonly LibraryContext _context;

        public ReadersController(LibraryContext context) {
            _context = context;
        }

        // GET: Readers
        public async Task<IActionResult> Index(string id = null) {
            // Sorting
            var orderBy = HttpContext.Request.Query["OrderBy"];
            ViewData["OrderBy"] = orderBy;
            ViewData["DetailOrderBy"] = "";
            ViewData["FirstNameSortParam"] = orderBy == "FirstName" ? "FirstName_desc" : "FirstName";
            ViewData["LastNameSortParam"] = orderBy == "LastName" ? "LastName_desc" : "LastName";
            ViewData["LoginSortParam"] = string.IsNullOrEmpty(orderBy) ? "Login_desc" : "";
            IQueryable<Reader> readers = _context.Reader
                        .Include(i => i.LoginNavigation);
            switch (orderBy) {
                case "FirstName_desc":
                    readers = readers.OrderByDescending(i => i.LoginNavigation.FirstName);
                    break;
                case "FirstName":
                    readers = readers.OrderBy(i => i.LoginNavigation.FirstName);
                    break;
                case "LastName_desc":
                    readers = readers.OrderByDescending(i => i.LoginNavigation.LastName);
                    break;
                case "LastName":
                    readers = readers.OrderBy(i => i.LoginNavigation.LastName);
                    break;
                case "Login_desc":
                    readers = readers.OrderByDescending(i => i.Login);
                    break;
                default:
                    readers = readers.OrderBy(i => i.Login);
                    break;
            }
            var viewModel = new ReadersViewModel {
                Readers = await readers.ToListAsync()
            };
            // Filtering
            var nameFilter = HttpContext.Request.Query["NameFilter"];
            viewModel.NameFilter = nameFilter;
            var loginFilter = HttpContext.Request.Query["LoginFilter"];
            viewModel.LoginFilter = loginFilter;
            var name = nameFilter.ToString()
                .ToLower()
                .Split(new char[0], StringSplitOptions.RemoveEmptyEntries)
                .ToHashSet();
            viewModel.Readers = viewModel.Readers
                .Where(i => {
                    if (!i.Login.ToLower().Contains(loginFilter.ToString().ToLower())) {
                        return false;
                    }
                    if (string.IsNullOrEmpty(nameFilter)) {
                        return true;
                    }
                    var match = string.Join(' ', i.LoginNavigation.FirstName, i.LoginNavigation.LastName)
                        .ToLower()
                        .Split(new char[0], StringSplitOptions.RemoveEmptyEntries)
                        .ToHashSet();
                    return name.IsSubsetOf(match);
                });
            // Detail view
            if (id != null) {
                viewModel.Selection = viewModel.Readers
                    .FirstOrDefault(i => i.Login == id);
                if (viewModel.Selection != null) {
                    var detailOrderBy = HttpContext.Request.Query["DetailOrderBy"];
                    ViewData["DetailOrderBy"] = detailOrderBy;
                    ViewData["BorrowingTitleSortParam"] = detailOrderBy == "BorrowingTitle" ?
                        "BorrowingTitle_desc" : "BorrowingTitle";
                    ViewData["BorrowDateSortParam"] = detailOrderBy == "BorrowDate_desc" ?
                        "BorrowDate" : "BorrowDate_desc";
                    ViewData["ReturnDateSortParam"] = (string.IsNullOrEmpty(detailOrderBy)
                        || detailOrderBy == "ReturnDate_desc") ? "ReturnDate" : "ReturnDate_desc";
                    ViewData["ReservationTitleSortParam"] = detailOrderBy == "ReservationTitle" ?
                        "ReservationTitle_desc" : "ReservationTitle";
                    ViewData["ReservationDateSortParam"] = string.IsNullOrEmpty(detailOrderBy) ?
                        "ReservationDate" : "";
                    IQueryable<Reservation> reservations = _context.Reservation
                        .Where(i => i.UserLogin == id)
                        .Include(i => i.Copy)
                            .ThenInclude(i => i.Edition)
                                .ThenInclude(i => i.Book);
                    IQueryable<Borrowing> borrowings = _context.Borrowing
                        .Where(i => i.UserLogin == id)
                        .Include(i => i.Copy)
                            .ThenInclude(i => i.Edition)
                                .ThenInclude(i => i.Book);
                    switch (detailOrderBy) {
                        case "ReservationTitle_desc":
                            reservations = reservations.OrderByDescending(i => i.Copy.Edition.Book.Title);
                            break;
                        case "ReservationTitle":
                            reservations = reservations.OrderBy(i => i.Copy.Edition.Book.Title);
                            break;
                        case "ReservationDate":
                            reservations = reservations.OrderBy(i => i.ReservationDate);
                            break;
                        default:
                            reservations = reservations.OrderByDescending(i => i.ReservationDate);
                            break;
                    }
                    switch (detailOrderBy) {
                        case "BorrowingTitle_desc":
                            borrowings = borrowings.OrderByDescending(i => i.Copy.Edition.Book.Title);
                            break;
                        case "BorrowingTitle":
                            borrowings = borrowings.OrderBy(i => i.Copy.Edition.Book.Title);
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
                    viewModel.Reservations = await reservations.ToListAsync();
                    viewModel.Borrowings = await borrowings.ToListAsync();
                }
            }

            return View(viewModel);
        }

        // GET: Readers/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Readers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Login,LoginNavigation,BirthDate")] Reader reader) {
            if (ModelState.IsValid) {
                _context.Add(reader.LoginNavigation);
                _context.Add(reader);
                try {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException) {
                    ViewData["errMsg"] = "Wybrany login jest już zajęty";
                }
            }
            return View(reader);
        }

        // GET: Readers/Edit/5
        public async Task<IActionResult> Edit(string id) {
            if (id == null) {
                return NotFound();
            }

            var reader = await _context.Reader
                .Include(i => i.LoginNavigation)
                .FirstOrDefaultAsync(i => i.Login == id);
            if (reader == null) {
                return NotFound();
            }
            return View(reader);
        }

        // POST: Readers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Login,LoginNavigation,BirthDate")] Reader reader) {
            if (id != reader.Login) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(reader.LoginNavigation);
                    _context.Update(reader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!ReaderExists(reader.Login)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reader);
        }

        // GET: Readers/Delete/5
        public async Task<IActionResult> Delete(string id) {
            if (id == null) {
                return NotFound();
            }

            var reader = await _context.Reader
                .Include(r => r.LoginNavigation)
                .FirstOrDefaultAsync(m => m.Login == id);
            if (reader == null) {
                return NotFound();
            }

            return View(reader);
        }

        // POST: Readers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id) {
            var reader = await _context.User.FindAsync(id);
            _context.User.Remove(reader);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReaderExists(string id) {
            return _context.Reader.Any(e => e.Login == id);
        }
    }
}
