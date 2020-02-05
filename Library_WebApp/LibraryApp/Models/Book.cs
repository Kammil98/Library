using System;
using System.Collections.Generic;

namespace LibraryApp.Models
{
    public partial class Book
    {
        public Book()
        {
            Authorship = new HashSet<Authorship>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }

        public Genre GenreNavigation { get; set; }
        public Edition Edition { get; set; }
        public ICollection<Authorship> Authorship { get; set; }
    }
}
