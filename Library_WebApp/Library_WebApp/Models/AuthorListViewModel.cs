using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class AuthorListViewModel
    {
        [Display(Name = "Imię")]
        public string SearchFirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string SearchLastName { get; set; }
        [Display(Name = "Kraj")]
        public string SearchCountry { get; set; }
        public Author author { get; set; }
        public List<Author> Authors { get; set; }
        public List<Property> Properties { get; set; }
        public List<Book> Books { get; set; }
        public AuthorListViewModel()
        {
            Books = new List<Book>();
            Authors = new List<Author>();
            Properties = new List<Property>();
        }
    }
}
