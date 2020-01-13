using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Librarian : User
    {
        [Display(Name = "Data zatrudnienia")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime? dateOfHire { set; get; }
        [Display(Name = "Nr filii")]
        [Required]
        public int? libraryBranchNumber { set; get; }
    }
}
