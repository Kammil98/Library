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

    public class ReservationsController : Controller {

        private readonly LibraryContext _context;

        public ReservationsController(LibraryContext context) {
            _context = context;
        }

        // GET: Reservations
        public IActionResult Index(int? id) {
            return RedirectToAction(nameof(BookCopiesController.Details), "BookCopies", new { id });
        }

        // POST: Reservations/Return
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int copyId) {
            await _context.ReturnCopy(copyId);
            return RedirectToAction(nameof(Index), new { id = copyId });
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int copyId, string login) {
            await _context.ReserveCopy(copyId, login);
            return RedirectToAction(nameof(Index), new { id = copyId });
        }
    }
}
