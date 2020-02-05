using System;
using System.Collections.Generic;

namespace LibraryApp.Models
{
    public partial class Borrowing
    {
        public string UserLogin { get; set; }
        public int CopyId { get; set; }
        public DateTimeOffset BorrowDate { get; set; }
        public DateTimeOffset? ReturnDate { get; set; }

        public virtual BookCopy Copy { get; set; }
        public virtual Reader UserLoginNavigation { get; set; }
    }
}
