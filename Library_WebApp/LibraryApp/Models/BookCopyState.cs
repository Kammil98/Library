using System;
using System.Collections.Generic;

namespace LibraryApp.Models
{
    public partial class BookCopyState
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string State { get; set; }
    }
}
