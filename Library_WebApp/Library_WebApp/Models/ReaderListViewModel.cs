using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class ReaderListViewModel
    {
        [Display(Name = "Login")]
        public string SearchLogin { get; set; }
        [Display(Name = "Imię")]
        public string SearchFirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string SearchLastName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data urodzenia")]
        public DateTime SearchDate { get; set; }
        public List<PublishingHouse> PublishingHouses { get; set; }
        public List<Book> Books { get; set; }
        public List<Edition> Editions { get; set; }
        public List<Volume> Volumes { get; set; }
        public List<Reader> Readers { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<Borrow> Borrows { get; set; }
        public List<Borrow> BorrowsHistory { get; set; }
        public ReaderListViewModel()
        {
            PublishingHouses = new List<PublishingHouse>();
            Books = new List<Book>();
            Editions = new List<Edition>();
            Volumes = new List<Volume>();
            Readers = new List<Reader>();
            Reservations = new List<Reservation>();
            Borrows = new List<Borrow>();
            BorrowsHistory = new List<Borrow>();
        }
    }
    
}
