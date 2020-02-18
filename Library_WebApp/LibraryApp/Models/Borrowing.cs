using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models {
    public partial class Borrowing {
        public string UserLogin { get; set; }
        public int CopyId { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [DataType(DataType.DateTime)]
        [DateLessThanOrEqualToToday(ErrorMessage = "Data nie może przekraczać aktualnej daty")]
        public DateTimeOffset BorrowDate { get; set; }
        [DataType(DataType.DateTime)]
        [DateLessThanOrEqualToToday(ErrorMessage = "Data nie może przekraczać aktualnej daty")]
        public DateTimeOffset? ReturnDate { get; set; }

        public virtual BookCopy Copy { get; set; }
        public virtual Reader UserLoginNavigation { get; set; }
    }
}
