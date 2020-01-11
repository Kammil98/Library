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
        public String name { set; get; }
    }
}
