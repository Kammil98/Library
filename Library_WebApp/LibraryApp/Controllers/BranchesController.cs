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

    public class BranchesController : Controller {

        private readonly LibraryContext _context;

        public BranchesController(LibraryContext context) {
            _context = context;
        }

        // GET: Branches
        public async Task<IActionResult> Index(int? id = null) {
            // Sorting
            var orderBy = HttpContext.Request.Query["OrderBy"];
            ViewData["OrderBy"] = orderBy;
            ViewData["DetailOrderBy"] = "";
            ViewData["NameSortParam"] = orderBy == "Name" ? "Name_desc" : "Name";
            ViewData["NumberSortParam"] = "";
            if (string.IsNullOrEmpty(orderBy)) {
                orderBy = "BranchNumber";
                ViewData["NumberSortParam"] = "BranchNumber_desc";
            }
            BranchesViewModel viewModel;
            try {
                viewModel = new BranchesViewModel {
                    Branches = await _context.Branch
                        .Include(i => i.Address)
                        .OrderByQuery(orderBy)
                        .ToListAsync()
                };
            }
            catch (InvalidOperationException) {
                viewModel = new BranchesViewModel {
                    Branches = await _context.Branch
                        .Include(i => i.Address)
                        .OrderBy(i => i.BranchNumber)
                        .ToListAsync()
                };
            }
            // Filtering
            var nameFilter = HttpContext.Request.Query["NameFilter"];
            viewModel.NameFilter = nameFilter;
            var addressFilter = HttpContext.Request.Query["AddressFilter"];
            viewModel.AddressFilter = addressFilter;
            var name = nameFilter.ToString().ToLower();
            var address = addressFilter.ToString()
                .ToLower()
                .Split(new char[0], StringSplitOptions.RemoveEmptyEntries)
                .ToHashSet();
            viewModel.Branches = viewModel.Branches
                .Where(i => {
                    if (!i.Name.ToLower().StartsWith(name) && !i.BranchNumber.ToString().StartsWith(name)) {
                        return false;
                    }
                    if (string.IsNullOrEmpty(addressFilter)) {
                        return true;
                    }
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
            // Detail view
            if (id != null) {
                viewModel.Selection = viewModel.Branches
                    .FirstOrDefault(i => i.BranchNumber == id);
                if (viewModel.Selection != null) {
                    var detailOrderBy = HttpContext.Request.Query["DetailOrderBy"];
                    ViewData["DetailOrderBy"] = detailOrderBy;
                    ViewData["DateSortParam"] = detailOrderBy == "EmploymentDate" ?
                        "EmploymentDate_desc" : "EmploymentDate";
                    ViewData["FirstNameSortParam"] = detailOrderBy == "FirstName" ?
                        "FirstName_desc" : "FirstName";
                    ViewData["LastNameSortParam"] = string.IsNullOrEmpty(detailOrderBy) ? "LastName_desc" : "";
                    IQueryable<Librarian> librarians = _context.Librarian
                        .Where(i => i.BranchNumber == id)
                        .Include(i => i.LoginNavigation);
                    switch (detailOrderBy) {
                        case "EmploymentDate_desc":
                            librarians = librarians.OrderByDescending(i => i.EmploymentDate);
                            break;
                        case "EmploymentDate":
                            librarians = librarians.OrderBy(i => i.EmploymentDate);
                            break;
                        case "FirstName_desc":
                            librarians = librarians.OrderByDescending(i => i.LoginNavigation.FirstName);
                            break;
                        case "FirstName":
                            librarians = librarians.OrderBy(i => i.LoginNavigation.FirstName);
                            break;
                        case "LastName_desc":
                            librarians = librarians.OrderByDescending(i => i.LoginNavigation.LastName);
                            break;
                        default:
                            librarians = librarians.OrderBy(i => i.LoginNavigation.LastName);
                            break;
                    }
                    viewModel.Librarians = await librarians.ToListAsync();
                }
            }

            return View(viewModel);
        }

        // GET: Branches/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Branches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BranchNumber,Name,OpeningHours,Address")] Branch branch) {
            if (ModelState.IsValid) {
                var address = _context.Add(branch.Address);
                branch.AddressId = address.Entity.Id;
                branch.OpeningHours = branch.OpeningHours.Replace(Environment.NewLine, "\n");
                _context.Add(branch);
                try {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException) {
                    ViewData["errMsg"] = "Nie można utworzyć filii, ponieważ istnieje już filia o podanym numerze lub nazwie, albo podany adres jest już zajęty";
                }
            }
            return View(branch);
        }

        // GET: Branches/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var branch = await _context.Branch
                .Include(p => p.Address)
                .FirstOrDefaultAsync(m => m.BranchNumber == id);
            if (branch == null) {
                return NotFound();
            }
            return View(branch);
        }

        // POST: Branches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("BranchNumber,Name,OpeningHours,AddressId,Address")] Branch branch) {
            if (id != branch.BranchNumber) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    branch.Address.Id = branch.AddressId;
                    branch.OpeningHours = branch.OpeningHours.Replace(Environment.NewLine, "\n");
                    _context.Update(branch);
                    _context.Update(branch.Address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!BranchExists(branch.BranchNumber)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                catch (DbUpdateException) {
                    ViewData["errMsg"] = "Nie można edytować filii, ponieważ istnieje już filia o podanym numerze lub nazwie, albo podany adres jest już zajęty";
                    return View(branch);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }

        // GET: Branches/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var branch = await _context.Branch
                .Include(p => p.Address)
                .FirstOrDefaultAsync(m => m.BranchNumber == id);
            if (branch == null) {
                return NotFound();
            }

            return View(branch);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var branch = await _context.Branch
                .Include(p => p.Address)
                .FirstOrDefaultAsync(m => m.BranchNumber == id);
            _context.Branch.Remove(branch);
            _context.Address.Remove(branch.Address);
            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) {
                ViewData["errMsg"] = "Nie można usunąć filii, do której przypisane są egzemplarze lub pracownicy";
                return View(branch);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BranchExists(int id) {
            return _context.Branch.Any(e => e.BranchNumber == id);
        }
    }
}
