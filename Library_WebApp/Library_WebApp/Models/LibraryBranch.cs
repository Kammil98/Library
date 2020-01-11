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
        public int number { set; get; }
        public Address address { set; get; }
        [Display(Name = "Nazwa")]
        public String name { set; get; }
    }
}
