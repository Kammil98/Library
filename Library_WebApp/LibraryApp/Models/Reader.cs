using System;
using System.Collections.Generic;

namespace LibraryApp.Models
{
    public partial class Reader
    {
        public Reader()
        {
            Borrowing = new HashSet<Borrowing>();
            Reservation = new HashSet<Reservation>();
        }

        public string Login { get; set; }
        public DateTime BirthDate { get; set; }

        public User LoginNavigation { get; set; }
        public ICollection<Borrowing> Borrowing { get; set; }
        public ICollection<Reservation> Reservation { get; set; }
    }
}
