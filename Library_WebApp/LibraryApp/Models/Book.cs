using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models {
    public partial class Book {
        public Book() {
            Authorship = new HashSet<Authorship>();
            Edition = new HashSet<Edition>();
            Authors = new List<Author>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression(@"^[^ \t]*$", ErrorMessage = "Tytuł nie może być pusty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Genre { get; set; }

        public virtual Genre GenreNavigation { get; set; }
        public virtual ICollection<Authorship> Authorship { get; set; }
        public virtual ICollection<Edition> Edition { get; set; }

        [NotMapped]
        public virtual IList<Author> Authors { get; set; }
    }
}
