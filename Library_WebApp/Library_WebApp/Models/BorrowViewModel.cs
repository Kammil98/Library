using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class BorrowViewModel
    {
        public Volume volume { get; set; }
        public Edition edition { get; set; }
        public Book book { get; set; }
        [Display(Name = "Wypożyczający")]
        public Reader reader { get; set; }
        public List<Borrow> Borrows { get; set; }
        public PublishingHouse publishingHouse { get; set; }
        public BorrowViewModel()
        {
            Borrows = new List<Borrow>();
        }
    }
}
