using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Librarian : User
    {
        public DateTime dateOfHire { set; get; }
        public int libraryBranchNumber { set; get; }
    }
}
