using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models {
    public partial class BookCopy {
        public BookCopy() {
            Borrowing = new HashSet<Borrowing>();
            Reservation = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public int EditionId { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public int BranchNumber { get; set; }
        public string Condition { get; set; }

        public virtual Branch BranchNumberNavigation { get; set; }
        public virtual Edition Edition { get; set; }
        public virtual ICollection<Borrowing> Borrowing { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
