using System;
using System.Collections.Generic;

namespace LibraryApp.Models
{
    public partial class BookCopy
    {
        public BookCopy()
        {
            Borrowing = new HashSet<Borrowing>();
            Reservation = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public int EditionId { get; set; }
        public int BranchNumber { get; set; }
        public string Condition { get; set; }

        public Branch BranchNumberNavigation { get; set; }
        public Edition Edition { get; set; }
        public ICollection<Borrowing> Borrowing { get; set; }
        public ICollection<Reservation> Reservation { get; set; }
    }
}
