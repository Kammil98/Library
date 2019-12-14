using Library_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
namespace Library_WebApp.Controllers
    //At the top are lines of code with Author class, to show how to comunicate and send data to View
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
        public IActionResult publishingHouseList()
        {
            List<PublishingHouse> publishingHouses = new List<PublishingHouse>();
            PublishingHouse publishingHouse = new PublishingHouse();
            publishingHouse.address = new Address();
            publishingHouse.name = "wydawnictwoAlfa";
            publishingHouse.address.street = "Królowej Jadwigi";
            publishingHouse.address.Country = "Polska";
            publishingHouse.address.buildingNr = 32;
            publishingHouse.address.City = "Poznań";
            publishingHouse.address.zipCode = "61-871";
            publishingHouses.Add(publishingHouse);

            publishingHouse = new PublishingHouse();
            publishingHouse.address = new Address();
            publishingHouse.name = "wydawnictwoBeta";
            publishingHouse.address.street = "Towarowa";
            publishingHouse.address.Country = "Polska";
            publishingHouse.address.buildingNr = 1;
            publishingHouse.address.City = "Poznań";
            publishingHouse.address.zipCode = "61-652";
            publishingHouses.Add(publishingHouse);
            return View("~/Views/Login/List/publishingHouseList.cshtml", publishingHouses);
        }

        [HttpGet]
        public IActionResult editPublishingHouse(string id, string name, string Country, string City, string buildingNr, string zipCode)
        {
            return View("~/Views/Login/Create/PublishingHouseData.cshtml");
        }

        [HttpGet]
        public IActionResult authorList()
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
            return View("~/Views/Login/List/authorList.cshtml", authors);
        }

        [HttpGet]
        public IActionResult editAuthor(Author model)
        {
            return View("~/Views/Login/Create/authorData.cshtml", model);
        }

        [HttpGet]
        public IActionResult deleteAuthor(Author model)
        {
            return View("~/Views/Login/Delete/authorDelete.cshtml", model);
        }

        
        [HttpGet]
        public IActionResult showAuthor(Author model)
        {
            return View("~/Views/Login/Details/showAuthor.cshtml", model);
        }

        [HttpGet]
        public IActionResult addAuthor()
        {
            return View("~/Views/Login/Create/authorData.cshtml");
        }

        [HttpPost]
        public IActionResult addAuthor(Author model)
        {

            return View("~/Views/Login/Create/authorData.cshtml");
        }

        [HttpGet]
        public IActionResult addBook()
        {
            return View("~/Views/Login/Create/bookData.cshtml");
        }

        [HttpGet]
        public IActionResult addBorrow()
        {
            return View("~/Views/Login/Create/borrowData.cshtml");
        }

        [HttpGet]
        public IActionResult addEdition()
        {
            return View("~/Views/Login/Create/editionData.cshtml");
        }

        [HttpGet]
        public IActionResult addGenre()
        {
            return View("~/Views/Login/Create/genreData.cshtml");
        }

        [HttpGet]
        public IActionResult addLibrarian()
        {
            return View("~/Views/Login/Create/librarianData.cshtml");
        }

        [HttpGet]
        public IActionResult addLibraryBranch()
        {
            return View("~/Views/Login/Create/libraryBranchData.cshtml");
        }

        [HttpGet]
        public IActionResult addProperty()
        {
            return View("~/Views/Login/Create/propertyData.cshtml");
        }

        [HttpGet]
        public IActionResult addPublishingHouse()
        {
            return View("~/Views/Login/Create/publishingHouseData.cshtml");
        }

        [HttpGet]
        public IActionResult addReader()
        {
            return View("~/Views/Login/Create/readerData.cshtml");
        }

        [HttpGet]
        public IActionResult addReservation()
        {
            return View("~/Views/Login/Create/reservationData.cshtml");
        }

        [HttpGet]
        public IActionResult addVolume()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Kamil Luwański", Value = "0" });

            items.Add(new SelectListItem { Text = "Mateusz Bąk", Value = "1" });

            items.Add(new SelectListItem { Text = "Ktoś Ktosiowaty", Value = "2", Selected = true });

            ViewBag.LibraryBranchId = items;
            return View("~/Views/Login/Create/volumeData.cshtml");
        }
        [HttpPost]
        public IActionResult addVolume(Volume model, string MovieType)
        {

            return View("~/Views/Login/Create/volumeData.cshtml");
        }
    }
}