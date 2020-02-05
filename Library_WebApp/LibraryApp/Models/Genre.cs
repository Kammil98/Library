using System;
using System.Collections.Generic;

namespace LibraryApp.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Book = new HashSet<Book>();
        }

        public string Name { get; set; }

        public ICollection<Book> Book { get; set; }
    }
}
