using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Author
    {
        public int id { set; get; }
        [Display(Name = "Imię")]
        public String firstName { set; get; }
        [Display(Name = "Nazwisko")]
        public String lastName { set; get; }
        [Display(Name = "Kraj")]
        public String country { set; get; }
    }
}
