using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Library_WebApp.Models;

namespace Library_WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            //small test to frontend. Can be deleted
            User user = new User();
            ViewData["User"] = user;
            return View();
        }

        [HttpPost]
        public IActionResult Registration(User model)
        {
            //small test to frontend. Can be deleted
            if (!ModelState.IsValid)
            {
                return View();
            }

            Response.Redirect("/");
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User model)
        {
            return RedirectToAction("Index", "Login"); //View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
    }
}
