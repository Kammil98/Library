using System;
using System.Collections.Generic;

namespace LibraryApp.Models
{
    public partial class Branch
    {
        public Branch()
        {
            BookCopy = new HashSet<BookCopy>();
            Librarian = new HashSet<Librarian>();
        }

        public int BranchNumber { get; set; }
        public int AddressId { get; set; }
        public string Name { get; set; }
        public string OpeningHours { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<BookCopy> BookCopy { get; set; }
        public virtual ICollection<Librarian> Librarian { get; set; }
    }
}
