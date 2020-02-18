using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models {
    public partial class Genre {
        public Genre() {
            Book = new HashSet<Book>();
        }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression(@"^[^ \t]*$", ErrorMessage = "Nazwa nie może być pusta")]
        public string Name { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
}
