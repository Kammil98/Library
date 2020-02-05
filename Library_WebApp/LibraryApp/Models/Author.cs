using System;
using System.Collections.Generic;

namespace LibraryApp.Models
{
    public partial class Author
    {
        public Author()
        {
            Authorship = new HashSet<Authorship>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }

        public ICollection<Authorship> Authorship { get; set; }
    }
}
