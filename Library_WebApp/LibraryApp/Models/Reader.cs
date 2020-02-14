using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models {
    public partial class Reader {
        public Reader() {
            Borrowing = new HashSet<Borrowing>();
            Reservation = new HashSet<Reservation>();
        }

        public string Login { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public virtual User LoginNavigation { get; set; }
        public virtual ICollection<Borrowing> Borrowing { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
