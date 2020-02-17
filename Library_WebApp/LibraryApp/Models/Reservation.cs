using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models {
    public partial class Reservation {
        public string UserLogin { get; set; }
        public int CopyId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTimeOffset ReservationDate { get; set; }

        public virtual BookCopy Copy { get; set; }
        public virtual Reader UserLoginNavigation { get; set; }
    }
}
