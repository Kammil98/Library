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
        public string FirstName { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string LastName { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Authorship> Authorship { get; set; }
    }
}
