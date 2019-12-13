using Library_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        [HttpGet]
        public IActionResult lista()
        {
            List<Author> authors = new List<Author>();
            Author author = new Author();
            author.firstName = "Kamil";
            author.lastName = "Luwański";
            author.country = "Polska";
            authors.Add(author);

            author = new Author();
            author.firstName = "Mateusz";
            author.lastName = "Bąk";
            author.country = "Inna Polska";
            authors.Add(author);
            return View("~/Views/Login/Update/lista.cshtml", authors);
        }

        [HttpGet]
        public IActionResult Details()
        {
            Author author = new Author();
            author.firstName = "Kamil";
            author.lastName = "Luwański";
            author.country = "Polska";
            return View("~/Views/Login/Update/Details.cshtml", author);
        }

        [HttpPost]
        public IActionResult addAuthor(Author model)
        {

            return View("~/Views/Login/Create/addAuthor.cshtml");
        }
        [HttpGet]
        public IActionResult addAuthor()
        {
            return View("~/Views/Login/Create/addAuthor.cshtml");
        }

        [HttpPost]
        public IActionResult addBook(Book model)
        {

            return View("~/Views/Login/Create/addBook.cshtml");
        }

        [HttpGet]
        public IActionResult addBook()
        {
            return View("~/Views/Login/Create/addBook.cshtml");
        }

        [HttpPost]
        public IActionResult addBorrow(Borrow model)
        {

            return View("~/Views/Login/Create/addBorrow.cshtml");
        }

        [HttpGet]
        public IActionResult addBorrow()
        {
            return View("~/Views/Login/Create/addBorrow.cshtml");
        }

        [HttpPost]
        public IActionResult addEdition(Edition model)
        {

            return View("~/Views/Login/Create/addEdition.cshtml");
        }

        [HttpGet]
        public IActionResult addEdition()
        {
            return View("~/Views/Login/Create/addEdition.cshtml");
        }

        [HttpPost]
        public IActionResult addGenre(Genre model)
        {

            return View("~/Views/Login/Create/addGenre.cshtml");
        }

        [HttpGet]
        public IActionResult addGenre()
        {
            return View("~/Views/Login/Create/addGenre.cshtml");
        }

        [HttpPost]
        public IActionResult addLibrarian(Librarian model)
        {

            return View("~/Views/Login/Create/addLibrarian.cshtml");
        }

        [HttpGet]
        public IActionResult addLibrarian()
        {
            return View("~/Views/Login/Create/addLibrarian.cshtml");
        }

        [HttpPost]
        public IActionResult addLibraryBranch(LibraryBranch model)
        {

            return View("~/Views/Login/Create/addLibraryBranch.cshtml");
        }

        [HttpGet]
        public IActionResult addLibraryBranch()
        {
            return View("~/Views/Login/Create/addLibraryBranch.cshtml");
        }

        [HttpPost]
        public IActionResult addProperty(Property model)
        {

            return View("~/Views/Login/Create/addProperty.cshtml");
        }

        [HttpGet]
        public IActionResult addProperty()
        {
            return View("~/Views/Login/Create/addProperty.cshtml");
        }
        [HttpPost]
        public IActionResult addPublishingHouse(PublishingHouse model)
        {

            return View("~/Views/Login/Create/addPublishingHouse.cshtml");
        }

        [HttpGet]
        public IActionResult addPublishingHouse()
        {
            return View("~/Views/Login/Create/addPublishingHouse.cshtml");
        }

        [HttpPost]
        public IActionResult addReader(Reader model)
        {
            return View("~/Views/Login/Create/addReader.cshtml");
        }

        [HttpGet]
        public IActionResult addReader()
        {
            return View("~/Views/Login/Create/addReader.cshtml");
        }

        [HttpPost]
        public IActionResult addReservation(Reservation model)
        {

            return View("~/Views/Login/Create/addReservation.cshtml");
        }

        [HttpGet]
        public IActionResult addReservation()
        {
            return View("~/Views/Login/Create/addReservation.cshtml");
        }

        [HttpPost]
        public IActionResult addVolume(Volume model)
        {

            return View("~/Views/Login/Create/addVolume.cshtml");
        }

        [HttpGet]
        public IActionResult addVolume()
        {
            return View("~/Views/Login/Create/addVolume.cshtml");
        }
    }
}