using System;
using System.Collections.Generic;

namespace LibraryApp.Models
{
    public partial class Reservation
    {
        public string UserLogin { get; set; }
        public int CopyId { get; set; }
        public DateTimeOffset ReservationDate { get; set; }

        public BookCopy Copy { get; set; }
        public Reader UserLoginNavigation { get; set; }
    }
}
