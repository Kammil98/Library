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
        public List<Book> Books = new List<Book>();
        public List<PublishingHouse> PublishingHouses = new List<PublishingHouse>();
        public List<Volume> Volumes = new List<Volume>(); 
        public List<Edition> Editions = new List<Edition>();
        public List<Borrow> Borrows = new List<Borrow>();
        public List<Reservation> Reservations = new List<Reservation>();
        public List<Reader> Readers = new List<Reader>();
        public LoginController()
        {
            PublishingHouse pubHouse = new PublishingHouse();
            pubHouse.id = 1000;
            pubHouse.name = "alfa";
            pubHouse.address = new Address();
            pubHouse.address.street = "Królowej Jadwigi";
            pubHouse.address.Country = "Polska";
            pubHouse.address.buildingNr = 32;
            pubHouse.address.City = "Poznań";
            pubHouse.address.zipCode = "61-871";

            PublishingHouses.Add(pubHouse);
            pubHouse = new PublishingHouse();
            pubHouse.id = 2000;
            pubHouse.name = "beta";
            pubHouse.address = new Address();
            pubHouse.address.street = "Towarowa";
            pubHouse.address.Country = "Polska";
            pubHouse.address.buildingNr = 1;
            pubHouse.address.City = "Poznań";
            pubHouse.address.zipCode = "61-652";
            PublishingHouses.Add(pubHouse);

            pubHouse = new PublishingHouse();
            pubHouse.address = new Address();
            pubHouse.id = 3000;
            pubHouse.name = "wydawnictwoAlfa";
            pubHouse.address.street = "ZusamenStrasse";
            pubHouse.address.Country = "Niemcy";
            pubHouse.address.buildingNr = 7;
            pubHouse.address.City = "Berlin";
            pubHouse.address.zipCode = "03-371";
            PublishingHouses.Add(pubHouse);

            pubHouse = new PublishingHouse();
            pubHouse.address = new Address();
            pubHouse.id = 4000;
            pubHouse.name = "wydawnictwoBeta";
            pubHouse.address.street = "Towarowa";
            pubHouse.address.Country = "Canada";
            pubHouse.address.buildingNr = 15;
            pubHouse.address.City = "canadiańska";
            pubHouse.address.zipCode = "61-652";
            PublishingHouses.Add(pubHouse);

            Volume volume = new Volume();
            volume.id = 1;
            volume.editionId = 10;
            volume.libraryBranchId = 3;
            volume.condition = "dobry";
            volume.State = Volume.BookState.Aviable;
            Volumes.Add(volume);
            volume = new Volume();
            volume.id = 2;
            volume.editionId = 20;
            volume.libraryBranchId = 6;
            volume.condition = "zły";
            volume.State = Volume.BookState.Reserved;
            Volumes.Add(volume);

            Edition edition = new Edition();
            edition.id = 10;
            edition.publishingHouseId = 1000;
            edition.releaseDate = DateTime.Now;
            edition.bookId = 100;
            Editions.Add(edition);
            edition = new Edition();
            edition.id = 20;
            edition.publishingHouseId = 2000;
            edition.releaseDate = DateTime.Now;
            edition.bookId = 200;
            Editions.Add(edition);

            Book book = new Book();
            book.id = 100;
            book.name = "Algorytm Frodo";
            book.genre = "dramat";
            Author author = new Author();
            author.firstName = "Wiesława";
            author.lastName = "Szymborska";
            book.Authors.Add(author);
            author = new Author();
            author.firstName = "Jan";
            author.lastName = "Kochanowski";
            book.Authors.Add(author);
            Books.Add(book);

            book = new Book();
            book.id = 200;
            book.name = "Władca Pierścieni";
            book.genre = "fantasy";
            author = new Author();
            author.firstName = "Jan";
            author.lastName = "Twardowski";
            book.Authors.Add(author);
            Books.Add(book);

            Borrow borrow = new Borrow();
            borrow.id = 1;
            borrow.borrowDate = DateTime.Now;
            borrow.userLogin = "kajak";
            borrow.volumeId = 1;
            Borrows.Add(borrow);
            borrow = new Borrow();
            borrow.id = 2;
            borrow.borrowDate = DateTime.Now;
            borrow.userLogin = "Kamil";
            borrow.volumeId = 2;
            Borrows.Add(borrow);

            Reservation reservation = new Reservation();
            reservation = new Reservation();
            reservation.id = 6;
            reservation.reservationDate = DateTime.Now;
            reservation.userLogin = "kajakOdTyłu";
            reservation.volumeId = 2;
            Reservations.Add(reservation);
        

            Reader reader = new Reader();
            reader.login = "kajak";
            reader.dateOfBirth = DateTime.Now;
            reader.lastName = "Jakiśtamiński";
            reader.name = "Jaś";
            reader.password = "123start";
            Readers.Add(reader);
            reader = new Reader();
            reader.login = "student";
            reader.dateOfBirth = DateTime.Now;
            reader.lastName = "Luwański";
            reader.name = "Kamil";
            reader.password = "start456";
            Readers.Add(reader);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult addCopyofBook()
        {
            return View();
        }
        [HttpPost]
        [HttpGet] 
        public IActionResult publishingHouseList(publishingHouseListViewModel model, int? id)
        {
            if(model == null)
            {
                model = new publishingHouseListViewModel();
            }
            model.PublishingHouses = PublishingHouses;
            if(id != null)
            {
                ViewBag.PublishingHouseId = id;
                model.publishingHouse = model.PublishingHouses.Find(x => x.id == id);
                model.Editions = Editions;
                model.Books = Books;
            }
            return View("~/Views/Login/ComplexPages/publishingHouseList.cshtml", model);
        }

        [HttpGet]
        public IActionResult editPublishingHouse(string id, string name, string Country, string City, string buildingNr, string zipCode)
        {
            return View("~/Views/Login/Create/PublishingHouseData.cshtml");
        }
        

       [HttpGet]
        public IActionResult editAuthor(Author model)
        {
            return View("~/Views/Login/Create/authorData.cshtml", model);
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
            BookDataViewModel model = new BookDataViewModel();
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
                volume.editionId = 5;
                volume.condition = "dobry";
                volume.State = Volume.BookState.Aviable;
                model.Volumes.Add(volume);
                volume = new Volume();
                volume.libraryBranchId = 2;
                volume.editionId = 6;
                volume.condition = "zły";
                volume.State = Volume.BookState.Borrowed;
                model.Volumes.Add(volume);

                PublishingHouse pubHouse = new PublishingHouse();
                pubHouse.id = 100;
                pubHouse.name = "alfa";
                model.PublishingHouses.Add(pubHouse);
                pubHouse = new PublishingHouse();
                pubHouse.id = 200;
                pubHouse.name = "beta";
                model.PublishingHouses.Add(pubHouse);

                Edition edition = new Edition();
                edition.id = 5;
                edition.publishingHouseId = 200;
                model.Editions.Add(edition);
                edition = new Edition();
                edition.id = 6;
                edition.publishingHouseId = 100;
                model.Editions.Add(edition);
            }
            return View("~/Views/Login/ComplexPages/bookList.cshtml", model);
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

            return View("~/Views/Login/ComplexPages/bookList.cshtml", model);
        }

        
        
        [HttpGet]
        [HttpPost]
        public IActionResult borrowInfo(int id, BorrowInfoViewModel model)
        {
            if(model == null)
            {
                model = new BorrowInfoViewModel();
            }
            Volume volume = new Volume();
            volume.id = 1;
            volume.editionId = 2;
            volume.libraryBranchId = 3;
            volume.condition = "dobry";
            volume.State = Volume.BookState.Aviable;
            model.volume = volume;

            Edition edition = new Edition();
            edition.id = 2;
            edition.publishingHouseId = 100;
            edition.releaseDate = DateTime.Now;
            edition.bookId = 20;
            model.edition = edition;

            Book book = new Book();
            book.id = 20;
            book.name = "Algorytm Frodo";
            book.genre = "dramat";
            Author author = new Author();
            author.firstName = "Wiesława";
            author.lastName = "Szymborska";
            book.Authors.Add(author);
            author = new Author();
            author.firstName = "Jan";
            author.lastName = "Kochanowski";
            book.Authors.Add(author);
            model.book = book;
            
            PublishingHouse pubHouse = new PublishingHouse();
            pubHouse.id = 100;
            pubHouse.name = "alfa";
            model.publishingHouse = pubHouse;
            if(model.reader != null)
            {
                Reader reader = new Reader();
                reader.login = "kajak";
                reader.dateOfBirth = DateTime.Now;
                reader.lastName = "Jakiśtamiński";
                reader.name = "Jaś";
                reader.password = "123start";
                model.reader = reader;
            }
            

            Borrow borrow = new Borrow();
            borrow.id = 15;
            borrow.borrowDate = DateTime.Now;
            borrow.userLogin = "Zbysiu";
            borrow.volumeId = 1;
            model.Borrows.Add(borrow);
            borrow = new Borrow();
            borrow.id = 25;
            borrow.borrowDate = DateTime.Now;
            borrow.userLogin = "Adam";
            borrow.volumeId = 1;
            model.Borrows.Add(borrow);

            return View("~/Views/Login/ComplexPages/borrowInfo.cshtml", model);
        }
        [HttpPost]
        [HttpGet]
        public IActionResult addBorrow(BorrowDataViewModel model)
        {
            if(model == null)
            {
                model = new BorrowDataViewModel();
            }
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "1", Value = "1" });
            items.Add(new SelectListItem { Text = "2", Value = "2" });
            items.Add(new SelectListItem { Text = "3", Value = "3" });
            model.Volumes = items;
            return View("~/Views/Login/Create/borrowData.cshtml", model);
        }

        [HttpGet]
        [HttpPost]
        public IActionResult readerList(String login, ReaderListViewModel model)
        {
            ViewBag.login = login;
            if (model == null)
            {
                model = new ReaderListViewModel();
            }
            if(login != null)
            {
                PublishingHouse pubHouse = new PublishingHouse();
                pubHouse.id = 1000;
                pubHouse.name = "alfa";
                model.PublishingHouses.Add(pubHouse);
                pubHouse = new PublishingHouse();
                pubHouse.id = 2000;
                pubHouse.name = "beta";
                model.PublishingHouses.Add(pubHouse);

                Volume volume = new Volume();
                volume.id = 1;
                volume.editionId = 10;
                volume.libraryBranchId = 3;
                volume.condition = "dobry";
                volume.State = Volume.BookState.Aviable;
                model.Volumes.Add(volume);
                volume = new Volume();
                volume.id = 2;
                volume.editionId = 20;
                volume.libraryBranchId = 6;
                volume.condition = "zły";
                volume.State = Volume.BookState.Reserved;
                model.Volumes.Add(volume);

                Edition edition = new Edition();
                edition.id = 10;
                edition.publishingHouseId = 1000;
                edition.releaseDate = DateTime.Now;
                edition.bookId = 100;
                model.Editions.Add(edition);
                edition = new Edition();
                edition.id = 20;
                edition.publishingHouseId = 2000;
                edition.releaseDate = DateTime.Now;
                edition.bookId = 200;
                model.Editions.Add(edition);

                Book book = new Book();
                book.id = 100;
                book.name = "Algorytm Frodo";
                book.genre = "dramat";
                Author author = new Author();
                author.firstName = "Wiesława";
                author.lastName = "Szymborska";
                book.Authors.Add(author);
                author = new Author();
                author.firstName = "Jan";
                author.lastName = "Kochanowski";
                book.Authors.Add(author);
                model.Books.Add(book);

                book = new Book();
                book.id = 200;
                book.name = "Władca Pierścieni";
                book.genre = "fantasy";
                author = new Author();
                author.firstName = "Jan";
                author.lastName = "Twardowski";
                book.Authors.Add(author);
                model.Books.Add(book);

                Borrow borrow = new Borrow();
                borrow.id = 1;
                borrow.borrowDate = DateTime.Now;
                borrow.userLogin = "kajak";
                borrow.volumeId = 1;
                model.Borrows.Add(borrow);
                borrow = new Borrow();
                borrow.id = 2;
                borrow.borrowDate = DateTime.Now;
                borrow.userLogin = "Kamil";
                borrow.volumeId = 2;
                model.Borrows.Add(borrow);

                borrow = new Borrow();
                borrow.id = 3;
                borrow.borrowDate = DateTime.Now;
                borrow.returnDate = DateTime.Now;
                borrow.userLogin = "kajakHistoria";
                borrow.volumeId = 1;
                model.BorrowsHistory.Add(borrow);

                Reservation reservation = new Reservation();
                reservation = new Reservation();
                reservation.id = 6;
                reservation.reservationDate = DateTime.Now;
                reservation.userLogin = "kajakOdTyłu";
                reservation.volumeId = 2;
                model.Reservations.Add(reservation);
            }

            Reader reader = new Reader();
            reader.login = "kajak";
            reader.dateOfBirth = DateTime.Now;
            reader.lastName = "Jakiśtamiński";
            reader.name = "Jaś";
            reader.password = "123start";
            model.Readers.Add(reader);
            reader = new Reader();
            reader.login = "student";
            reader.dateOfBirth = DateTime.Now;
            reader.lastName = "Luwański";
            reader.name = "Kamil";
            reader.password = "start456";
            model.Readers.Add(reader);

            return View("~/Views/Login/ComplexPages/readerList.cshtml", model);
        }
        [HttpGet]
        [HttpPost]
        public IActionResult libraryBranchList(int? number, LibraryBranchListViewModel model)
        {
            if (model == null)
            {
                model = new LibraryBranchListViewModel();
            }
            LibraryBranch lb = new LibraryBranch();
            lb.number = 100;
            lb.name = "alfa";
            lb.address = new Address();
            lb.address.street = "Wronkowa";
            lb.address.zipCode = "61-871";
            lb.address.Country = "Polska";
            lb.address.City = "Poznań";
            lb.address.buildingNr = 5;
            model.LibraryBranchs.Add(lb);
            lb = new LibraryBranch();
            lb.number = 200;
            lb.name = "beta";
            lb.address = new Address();
            lb.address.street = "Królowej Jadwigi";
            lb.address.zipCode = "61-689";
            lb.address.Country = "Polska";
            lb.address.City = "Warszawa";
            lb.address.buildingNr = 32;
            model.LibraryBranchs.Add(lb);
            if(number != null)
            {
                model.libraryBranch = lb;
                Librarian librarian = new Librarian();
                librarian.login = "kajak";
                librarian.dateOfHire = DateTime.Now;
                librarian.lastName = "Jakiśtamiński";
                librarian.name = "Jaś";
                librarian.password = "123start";
                model.Librarians.Add(librarian);
                librarian = new Librarian();
                librarian.login = "student";
                librarian.dateOfHire = DateTime.Now;
                librarian.lastName = "Luwański";
                librarian.name = "Kamil";
                librarian.password = "start456";
                model.Librarians.Add(librarian);
            }
            return View("~/Views/Login/ComplexPages/LibraryBranchList.cshtml", model);
        }
        [HttpGet]
        public IActionResult addEdition()
        {
            EditionDataViewModel model = new EditionDataViewModel();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Władca Pierścieni", Value = "1" });
            items.Add(new SelectListItem { Text = "Algorytm Frodo", Value = "2" });
            items.Add(new SelectListItem { Text = "Moja autobiografia", Value = "3"});
            model.Books = items;
            items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Wydawnictwo alfa", Value = "1" });
            items.Add(new SelectListItem { Text = "Wydawnictwo beta", Value = "2" });
            items.Add(new SelectListItem { Text = "Wydawnictwo charlie", Value = "3"});
            model.PublishingHouses = items;
            return View("~/Views/Login/Create/editionData.cshtml", model);
        }
        [HttpPost]
        public IActionResult addEdition(Edition model, string Books)
        {
            return View("~/Views/Login/Create/editionData.cshtml", model);
        }
        [HttpGet]
        public IActionResult editVolume(int? id)
        {
            return View("~/Views/Login/Create/editionData.cshtml");
        }
        [HttpGet]
        public IActionResult addGenre()
        {
            return View("~/Views/Login/Create/genreData.cshtml");
        }

        [HttpPost]
        [HttpGet]
        public IActionResult addLibrarian(LibrarianDataViewModel model)
        {
            if(model == null)
            {
                model = new LibrarianDataViewModel();
            }
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

        [HttpPost]
        [HttpGet]
        public IActionResult addProperty(PropertyDataViewModel model)
        {
            if(model == null)
            {
                model = new PropertyDataViewModel();
            }
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Kamil Luwański", Value = "1" });
            items.Add(new SelectListItem { Text = "Mateusz Bąk", Value = "2" });
            items.Add(new SelectListItem { Text = "Ktoś Ktosiowaty", Value = "3"});
            model.Authors = items;
            items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Władca Pierścieni", Value = "4" });
            items.Add(new SelectListItem { Text = "Algorytm Frodo", Value = "5" });
            items.Add(new SelectListItem { Text = "Moja autobiografia", Value = "6" });
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
        [HttpPost]
        [HttpGet]
        public IActionResult addVolume(VolumeDataViewModel model)
        {
            if(model == null)
            {
                model = new VolumeDataViewModel();
            }
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Wydanie 1", Value = "1" });
            items.Add(new SelectListItem { Text = "Wydanie 2", Value = "2" });
            items.Add(new SelectListItem { Text = "Wydanie 3", Value = "3"});
            model.Editions = items;
            items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "4", Value = "4" });
            items.Add(new SelectListItem { Text = "5", Value = "5" });
            items.Add(new SelectListItem { Text = "6", Value = "6"});
            model.LibraryBranchIds = items;
            return View("~/Views/Login/Create/volumeData.cshtml", model);
        }
    }
}