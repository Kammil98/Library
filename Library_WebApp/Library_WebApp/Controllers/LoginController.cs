using Library_WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_WebApp.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult addCopyofBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult addBorrow(Borrow model)
        {

            return View();
        }

        [HttpGet]
        public IActionResult addBorrow()
        {
            return View();
        }

        [HttpPost]
        public IActionResult addReservation(Reservation model)
        {

            return View();
        }

        [HttpGet]
        public IActionResult addReservation()
        {
            return View();
        }
    }
}