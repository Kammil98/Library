using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Models {
    public class BookCopiesViewModel {
        public BookCopy Copy { get; set; }
        public Reader Reader { get; set; }
        public string ReaderQuery { get; set; }
        public IEnumerable<Borrowing> Borrowings { get; set; }
    }
}
