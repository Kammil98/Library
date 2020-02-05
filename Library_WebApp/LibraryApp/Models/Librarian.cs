using System;
using System.Collections.Generic;

namespace LibraryApp.Models
{
    public partial class Librarian
    {
        public string Login { get; set; }
        public DateTime EmploymentDate { get; set; }
        public int BranchNumber { get; set; }

        public virtual Branch BranchNumberNavigation { get; set; }
        public virtual User LoginNavigation { get; set; }
    }
}
