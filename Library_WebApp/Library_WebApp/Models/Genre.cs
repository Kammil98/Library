using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Genre
    {
        [Display(Name = "Gatunek")]
        [Required(ErrorMessage = "Pole {0} nie może być puste.")]
        public String name { set; get; }
    }
}
