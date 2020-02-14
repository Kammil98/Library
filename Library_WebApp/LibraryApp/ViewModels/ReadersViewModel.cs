using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Models {
    public class ReadersViewModel {
        public IEnumerable<Reader> Readers { get; set; }
        public Reader Selection { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
        public IEnumerable<Borrowing> Borrowings { get; set; }

        [Display(Name = "Imię i/lub nazwisko")]
        public string NameFilter { get; set; }
        [Display(Name = "Login")]
        public string LoginFilter { get; set; }
    }
}
