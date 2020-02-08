using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryApp;
using LibraryApp.Models;
using System.Text;

namespace LibraryApp.Controllers {

    public class BooksController : Controller {

        private readonly LibraryContext _context;

        public BooksController(LibraryContext context) {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(int? id = null) {
            var viewModel = new BooksViewModel();
            // Filtering
            var available = HttpContext.Request.Query["Available"];
            viewModel.Available = available.Contains("True") || !available.Any();
            var reserved = HttpContext.Request.Query["Reserved"];
            viewModel.Reserved = reserved.Contains("True") || !reserved.Any();
            var borrowed = HttpContext.Request.Query["Borrowed"];
            viewModel.Borrowed = borrowed.Contains("True") || !borrowed.Any();
            var titleFilter = HttpContext.Request.Query["TitleFilter"];
            viewModel.TitleFilter = titleFilter;
            var authorFilter = HttpContext.Request.Query["AuthorFilter"];
            viewModel.AuthorFilter = authorFilter;
            var publishingHouseFilter = HttpContext.Request.Query["PublishingHouseFilter"];
            viewModel.PublishingHouseFilter = publishingHouseFilter;
            var branchFilter = HttpContext.Request.Query["BranchFilter"];
            viewModel.BranchFilter = branchFilter;
            var genreFilter = HttpContext.Request.Query["GenreFilter"];
            viewModel.GenreFilter = genreFilter;
            var branch = await _context.Branch.Where(i => i.Name == branchFilter.ToString()).FirstOrDefaultAsync();
            int? branchNumber = branch?.BranchNumber;
            IQueryable<Book> books;
            if (!viewModel.Available && !viewModel.Reserved && !viewModel.Borrowed) {
                books = _context.FilterBooks(titleFilter, publishingHouseFilter, genreFilter, branchNumber, null);
            }
            else {
                books = _context.Book.Take(0);
                if (viewModel.Available) {
                    books = books.Union(_context.FilterBooks(titleFilter, publishingHouseFilter, genreFilter, branchNumber, "available"));
                }
                if (viewModel.Reserved) {
                    books = books.Union(_context.FilterBooks(titleFilter, publishingHouseFilter, genreFilter, branchNumber, "reserved"));
                }
                if (viewModel.Borrowed) {
                    books = books.Union(_context.FilterBooks(titleFilter, publishingHouseFilter, genreFilter, branchNumber, "borrowed"));
                }
            }
            // Sorting
            var orderBy = HttpContext.Request.Query["OrderBy"];
            ViewData["OrderBy"] = orderBy;
            ViewData["DetailOrderBy"] = "";
            ViewData["GenreSortParam"] = orderBy == "Genre" ? "Genre_desc" : "Genre";
            ViewData["TitleSortParam"] = "";
            if (string.IsNullOrEmpty(orderBy)) {
                orderBy = "Title";
                ViewData["TitleSortParam"] = "Title_desc";
            }
            try {
                viewModel.Books = await books
                        .OrderByQuery(orderBy)
                        .ToListAsync();
            }
            catch (InvalidOperationException) {
                viewModel.Books = await books
                        .OrderBy(i => i.Title)
                        .ToListAsync();
            }
            // Filtering (authors)
            var authors = authorFilter.ToString()
                .ToLower()
                .Split(new char[0], StringSplitOptions.RemoveEmptyEntries)
                .ToHashSet();
            if (authors.Any()) {
                viewModel.Books = viewModel.Books
                    .Where(i => {
                        var bookAuthors = _context.BookAuthors(i.Id).ToList();
                        var matchBuilder = new StringBuilder();
                        foreach (var author in bookAuthors) {
                            matchBuilder.AppendJoin(' ', author.LastName, author.FirstName);
                        }
                        var match = matchBuilder.ToString()
                            .ToLower()
                            .Split(new char[0], StringSplitOptions.RemoveEmptyEntries)
                            .ToHashSet();
                        return authors.IsSubsetOf(match);
                    });
            }
            // Detail view
            if (id != null) {
                viewModel.Selection = viewModel.Books
                    .FirstOrDefault(i => i.Id == id);
                if (viewModel.Selection != null) {
                    var detailOrderBy = HttpContext.Request.Query["DetailOrderBy"];
                    ViewData["DetailOrderBy"] = detailOrderBy;
                    ViewData["StateSortParam"] = detailOrderBy == "State" ?
                        "State_desc" : "State";
                    ViewData["PublishingHouseSortParam"] = detailOrderBy == "PublishingHouse" ?
                        "PublishingHouse_desc" : "PublishingHouse";
                    ViewData["BranchSortParam"] = string.IsNullOrEmpty(detailOrderBy) ? "Branch_desc" : "";
                    viewModel.States = await _context.BookCopyState
                        .Where(i => i.BookId == viewModel.Selection.Id)
                        .ToDictionaryAsync(i => i.Id, i => i.State);
                    viewModel.Authors = await _context.BookAuthors(viewModel.Selection.Id).ToListAsync();
                    var copies = await _context.BookCopy
                        .Include(i => i.Edition)
                        .Include(i => i.BranchNumberNavigation)
                        .Where(i => i.Edition.BookId == id)
                        .ToListAsync();
                    viewModel.CopiesCount = copies.Count();
                    viewModel.CopiesAvailable = copies.Count(i => viewModel.States[i.Id] == "available");
                    var publishingHouse = !publishingHouseFilter.Any() ? "" : publishingHouseFilter.ToString();
                    var filtered = copies
                        .Where(i => branchNumber == null || i.BranchNumber == branchNumber)
                        .Where(i => i.Edition.PublishingHouse.StartsWith(publishingHouse));
                    if (!viewModel.Available) {
                        filtered = filtered.Except(filtered.Where(i => viewModel.States[i.Id] == "available"));
                    }
                    if (!viewModel.Reserved) {
                        filtered = filtered.Except(filtered.Where(i => viewModel.States[i.Id] == "reserved"));
                    }
                    if (!viewModel.Borrowed) {
                        filtered = filtered.Except(filtered.Where(i => viewModel.States[i.Id] == "borrowed"));
                    }
                    switch (detailOrderBy) {
                        case "State_desc":
                            filtered = filtered.OrderByDescending(i => viewModel.States[i.Id]);
                            break;
                        case "State":
                            filtered = filtered.OrderBy(i => viewModel.States[i.Id]);
                            break;
                        case "PublishingHouse_desc":
                            filtered = filtered.OrderByDescending(i => i.Edition.PublishingHouse);
                            break;
                        case "PublishingHouse":
                            filtered = filtered.OrderBy(i => i.Edition.PublishingHouse);
                            break;
                        case "Branch_desc":
                            filtered = filtered.OrderByDescending(i => i.BranchNumberNavigation.Name);
                            break;
                        default:
                            filtered = filtered.OrderBy(i => i.BranchNumberNavigation.Name);
                            break;
                    }
                    viewModel.Copies = filtered;
                }
            }

            ViewData["Branch"] = new SelectList(_context.Branch, "Name", "Name", branchFilter);
            ViewData["Genre"] = new SelectList(_context.Genre, "Name", "Name", genreFilter);
            return View(viewModel);
        }

        // GET: Books/Create
        public IActionResult Create() {
            ViewData["Genre"] = new SelectList(_context.Genre, "Name", "Name");
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Genre")] Book book) {
            if (ModelState.IsValid) {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Genre"] = new SelectList(_context.Genre, "Name", "Name", book.Genre);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null) {
                return NotFound();
            }
            ViewData["Genre"] = new SelectList(_context.Genre, "Name", "Name", book.Genre);
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre")] Book book) {
            if (id != book.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!BookExists(book.Id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Genre"] = new SelectList(_context.Genre, "Name", "Name", book.Genre);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.GenreNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null) {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id) {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
