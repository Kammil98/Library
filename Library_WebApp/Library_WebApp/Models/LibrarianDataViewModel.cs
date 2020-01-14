using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class LibrarianDataViewModel
    {
        public Librarian librarian { get; set; }
        public List<SelectListItem> libraryBranchIds { get; set; }
    }
}
