using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class LibraryBranchListViewModel
    {
        [Range(0, int.MaxValue)]
        [Display(Name = "Nr filii")]
        public int? SearchLibraryBranchId { get; set; }
        [Display(Name = "Nazwa filii")]
        public string SearchLibraryname { get; set; }
        [Display(Name = "Imię pracownika")]
        public string SearchLibrarianFirstName { get; set; }
        [Display(Name = "nazwisko pracownika")]
        public string SearchLibrarianLastName { get; set; }
        public LibraryBranch libraryBranch { get; set; }
        public List<LibraryBranch> LibraryBranchs { get; set; }
        public List<Librarian> Librarians { get; set; }
        public LibraryBranchListViewModel()
        {
            LibraryBranchs = new List<LibraryBranch>();
            Librarians = new List<Librarian>();
        }
    }
}
