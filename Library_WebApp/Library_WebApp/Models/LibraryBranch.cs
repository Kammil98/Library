using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class LibraryBranch
    {
        [Display(Name = "Nr filii")]
        [Required(ErrorMessage = "Pole {0} nie może być puste.")]
        public int number { set; get; }
        public Address address { set; get; }
        [Display(Name = "Nazwa filii")]
        [Required(ErrorMessage = "Pole {0} nie może być puste.")]
        public String name { set; get; }
    }
}
