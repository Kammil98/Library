using System;
using System.Collections.Generic;

namespace LibraryApp.Models
{
    public partial class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Librarian Librarian { get; set; }
        public virtual Reader Reader { get; set; }
    }
}
