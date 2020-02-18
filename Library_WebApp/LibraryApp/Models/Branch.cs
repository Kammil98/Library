using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models {
    public partial class Branch {
        public Branch() {
            BookCopy = new HashSet<BookCopy>();
            Librarian = new HashSet<Librarian>();
        }

        [Required(ErrorMessage = "To pole jest wymagane")]
        public int BranchNumber { get; set; }
        public int AddressId { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression(@"^[^ \t]*$", ErrorMessage = "Nazwa nie może być pusta")]
        public string Name { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string OpeningHours { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<BookCopy> BookCopy { get; set; }
        public virtual ICollection<Librarian> Librarian { get; set; }
    }
}
