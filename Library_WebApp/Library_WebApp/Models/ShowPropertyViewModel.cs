using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class ShowPropertyViewModel
    {
        public Property property { get; set; }
        [Display(Name = "Tytuł książki")]
        public string bookTitle { get; set; }
        [Display(Name = "Autorzy")]
        public List<Author> Authors { get; set; }
        public ShowPropertyViewModel()
        {
            Authors = new List<Author>();
            property = new Property();
        }
    }
}
