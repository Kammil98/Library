using Library_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [HttpPost]
        public IActionResult authorList(string Bookphrase, string Volumephrase)
        {
            List<Author> authors = new List<Author>();
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
            List<Book> books = new List<Book>();
            Book book = new Book();
            book.name = "Władca Pierścieni";
            book.genre = "fantasy";
            books.Add(book);

            book = new Book();
            book.name = "Algorytm Frodo";
            book.genre = "dramat";
            books.Add(book);

            List<Volume> volumes = new List<Volume>();
            Volume volume = new Volume();
            volume.id = 1;
            volume.libraryBranchId = 2;
            volume.editionId = 3;
            volume.condition = "uzywana";

            volumes.Add(volume);
            ViewData["Books"] = books;
            ViewData["Volumes"] = volumes;
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
            Book model = new Book();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "epika", Value = "epika" });
            items.Add(new SelectListItem { Text = "liryka", Value = "liryka" });
            items.Add(new SelectListItem { Text = "dramat", Value = "dramat" });
            model.Genres = items;
            return View("~/Views/Login/Create/bookData.cshtml", model);
        }

        [HttpGet]
        public IActionResult bookList(int? BookId)
        {
            BooksListViewModel model = new BooksListViewModel();
            Book book = new Book();
            book.id = 10;
            book.name = "Władca Pierścieni";
            book.genre = "fantasy";
            model.Books.Add(book);
            book = new Book();
            book.id = 20;
            book.name = "Algorytm Frodo";
            book.genre = "dramat";
            model.Books.Add(book);

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "epika", Value = "epika" });
            items.Add(new SelectListItem { Text = "liryka", Value = "liryka" });
            items.Add(new SelectListItem { Text = "dramat", Value = "dramat" });
            items.Add(new SelectListItem { Text = "fantasy", Value = "fantasy" });
            model.Genres = items;

            items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "1", Value = "0" });
            items.Add(new SelectListItem { Text = "2", Value = "1" });
            items.Add(new SelectListItem { Text = "3", Value = "2" });
            model.LibraryBranchIds = items;
            if(BookId != null)
            {
                model.Volumes = new List<Volume>();
                ViewBag.BookId = BookId;
                Volume volume = new Volume();
                volume.libraryBranchId = 1;
                volume.condition = "dobry";
                volume.State = Volume.BookState.Aviable;
                model.Volumes.Add(volume);
                volume = new Volume();
                volume.libraryBranchId = 2;
                volume.condition = "zły";
                volume.State = Volume.BookState.Borrowed;
                model.Volumes.Add(volume);

                PublishingHouse pubHouse = new PublishingHouse();
                pubHouse.id = 1;
                pubHouse.name = "alfa";
                model.PublishingHouses.Add(pubHouse);
                pubHouse = new PublishingHouse();
                pubHouse.id = 2;
                pubHouse.name = "beta";
                model.PublishingHouses.Add(pubHouse);
            }
            return View("~/Views/Login/List/bookList.cshtml", model);
        }

        [HttpPost]
        public IActionResult bookList(BooksListViewModel model)
        {
            Book book = new Book();
            book.name = "Władca Pierścieni";
            book.genre = "fantasy";
            model.Books.Add(book);
            book = new Book();
            book.name = "Algorytm Frodo";
            book.genre = "dramat";
            model.Books.Add(book);

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "epika", Value = "epika" });
            items.Add(new SelectListItem { Text = "liryka", Value = "liryka" });
            items.Add(new SelectListItem { Text = "dramat", Value = "dramat" });
            items.Add(new SelectListItem { Text = "fantasy", Value = "fantasy" });
            model.Genres = items;

            items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "1", Value = "0" });
            items.Add(new SelectListItem { Text = "2", Value = "1" });
            items.Add(new SelectListItem { Text = "3", Value = "2" });
            model.LibraryBranchIds = items;

            return View("~/Views/Login/List/bookList.cshtml", model);
        }

        [HttpGet]
        public IActionResult addBorrow()
        {
            return View("~/Views/Login/Create/borrowData.cshtml");
        }

        [HttpGet]
        public IActionResult addEdition()
        {
            Edition model = new Edition();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Władca Pierścieni", Value = "Władca Pierścieni" });
            items.Add(new SelectListItem { Text = "Algorytm Frodo", Value = "Algorytm Frodo" });
            items.Add(new SelectListItem { Text = "Moja autobiografia", Value = "Moja autobiografia"});
            model.Books = items;
            items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Wydawnictwo alfa", Value = "Wydawnictwo alfa" });
            items.Add(new SelectListItem { Text = "Wydawnictwo beta", Value = "Wydawnictwo beta" });
            items.Add(new SelectListItem { Text = "Wydawnictwo charlie", Value = "Wydawnictwo charlie"});
            model.PublishingHouses = items;
            return View("~/Views/Login/Create/editionData.cshtml", model);
        }
        [HttpPost]
        public IActionResult addEdition(Edition model, string Books)
        {
            return View("~/Views/Login/Create/editionData.cshtml", model);
        }

        [HttpGet]
        public IActionResult addGenre()
        {
            return View("~/Views/Login/Create/genreData.cshtml");
        }

        [HttpGet]
        public IActionResult addLibrarian()
        {
            Librarian model = new Librarian();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "1", Value = "0" });
            items.Add(new SelectListItem { Text = "2", Value = "1" });
            items.Add(new SelectListItem { Text = "3", Value = "2" });
            model.libraryBranchIds = items;
            return View("~/Views/Login/Create/librarianData.cshtml", model);
        }

        [HttpGet]
        public IActionResult addLibraryBranch()
        {
            return View("~/Views/Login/Create/libraryBranchData.cshtml");
        }

        [HttpGet]
        public IActionResult addProperty()
        {
            Property model = new Property();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Kamil Luwański", Value = "Kamil Luwański" });
            items.Add(new SelectListItem { Text = "Mateusz Bąk", Value = "Mateusz Bąk" });
            items.Add(new SelectListItem { Text = "Ktoś Ktosiowaty", Value = "Ktoś Ktosiowaty"});
            model.Authors = items;
            items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Władca Pierścieni", Value = "Władca Pierścieni" });
            items.Add(new SelectListItem { Text = "Algorytm Frodo", Value = "Algorytm Frodo" });
            items.Add(new SelectListItem { Text = "Moja autobiografia", Value = "Moja autobiografia" });
            model.Books = items;
            return View("~/Views/Login/Create/propertyData.cshtml", model);
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
            Volume model = new Volume();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Wydanie 1", Value = "0" });
            items.Add(new SelectListItem { Text = "Wydanie 2", Value = "1" });
            items.Add(new SelectListItem { Text = "Wydanie 3", Value = "2"});
            model.Editions = items;
            items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "1", Value = "0" });
            items.Add(new SelectListItem { Text = "2", Value = "1" });
            items.Add(new SelectListItem { Text = "3", Value = "2"});
            model.LibraryBranchIds = items;
            return View("~/Views/Login/Create/volumeData.cshtml", model);
        }
        [HttpPost]
        public IActionResult addVolume(Volume model, string MovieType)
        {

            return View("~/Views/Login/Create/volumeData.cshtml");
        }
    }
}