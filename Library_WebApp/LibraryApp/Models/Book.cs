using System;
using System.Collections.Generic;

namespace LibraryApp.Models
{
    public partial class Book
    {
        public Book()
        {
            Authorship = new HashSet<Authorship>();
            Edition = new HashSet<Edition>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }

        public virtual Genre GenreNavigation { get; set; }
        public virtual ICollection<Authorship> Authorship { get; set; }
        public virtual ICollection<Edition> Edition { get; set; }
    }
}
