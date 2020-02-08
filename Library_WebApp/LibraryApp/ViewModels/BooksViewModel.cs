using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Models {
    public class BooksViewModel {
        public IEnumerable<Book> Books { get; set; }
        public Book Selection { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<BookCopy> Copies { get; set; }
        public IDictionary<int, string> States { get; set; }
        public int CopiesCount { get; set; }
        public int CopiesAvailable { get; set; }

        [Display(Name = "Tytuł")]
        public string TitleFilter { get; set; }
        [Display(Name = "Autor")]
        public string AuthorFilter { get; set; }
        [Display(Name = "Gatunek")]
        public string GenreFilter { get; set; }
        [Display(Name = "Filia")]
        public string BranchFilter { get; set; }
        [Display(Name = "Wydawnictwo")]
        public string PublishingHouseFilter { get; set; }
        public bool Available { get; set; } = true;
        public bool Reserved { get; set; } = true;
        public bool Borrowed { get; set; } = true;
    }
}
