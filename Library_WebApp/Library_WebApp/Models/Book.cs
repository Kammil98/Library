using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Book
    {
        public int id { set; get; }
        [Display(Name = "Tytuł")]
        [Required(ErrorMessage = "Pole {0} nie może być puste.")]
        public String name { set; get; }
        [Display(Name = "Gatunek")]
        [Required(ErrorMessage = "Pole {0} nie może być puste.")]
        public String genre { set; get; }
        [Display(Name = "Autorzy")]
        public List<Author> Authors { get; set; }
        public Book()
        {
            Authors = new List<Author>();
        }
    }
}
