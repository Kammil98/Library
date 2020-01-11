using Microsoft.AspNetCore.Mvc.Rendering;
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
        public String name { set; get; }
        [Display(Name = "Gatunek")]
        public String genre { set; get; }
        [Display(Name = "Autorzy")]
        public List<Author> Authors { get; set; }
        public List<SelectListItem> Genres { get; set; }
        public Book()
        {
            Authors = new List<Author>();
        }
    }
}
