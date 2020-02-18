using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models {
    public partial class Author {
        public Author() {
            Authorship = new HashSet<Authorship>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression(@"^[A-Z][a-zA-Z \-]*$", ErrorMessage = "Imię zawiera niepoprawne znaki")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression(@"^[A-Z][a-zA-Z \-]*$", ErrorMessage = "Nazwisko zawiera niepoprawne znaki")]
        public string LastName { get; set; }
        [RegularExpression(@"^[A-Z][a-zA-Z \-]*$", ErrorMessage = "Nazwa kraju zawiera niepoprawne znaki")]
        public string Country { get; set; }

        public virtual ICollection<Authorship> Authorship { get; set; }
    }
}
