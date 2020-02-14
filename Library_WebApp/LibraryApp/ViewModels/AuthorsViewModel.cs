using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Models {
    public class AuthorsViewModel {
        public IEnumerable<Author> Authors { get; set; }
        public Author Selection { get; set; }
        public IEnumerable<Book> Books { get; set; }

        [Display(Name = "Imię i/lub nazwisko")]
        public string NameFilter { get; set; }
        [Display(Name = "Kraj")]
        public string CountryFilter { get; set; }
    }
}
