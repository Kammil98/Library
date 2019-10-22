using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
            User user = new User();
            user.name = "Ja";
            ViewData["User"] = user;
            return View();
        }

        [HttpPost]
        public IActionResult Registration(string firstName)
        {
            System.Console.WriteLine("firstName = " + firstName);
            if (firstName != null)
            {
                Response.Redirect("/");
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
