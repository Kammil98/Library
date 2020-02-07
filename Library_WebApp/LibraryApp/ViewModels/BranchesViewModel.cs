using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Models {
    public class BranchesViewModel {
        public IEnumerable<Branch> Branches { get; set; }
        public Branch Selection { get; set; }
        public IEnumerable<Librarian> Librarians { get; set; }

        [Display(Name = "Nazwa lub numer filii")]
        public string NameFilter { get; set; }
        [Display(Name = "Adres")]
        public string AddressFilter { get; set; }
    }
}
