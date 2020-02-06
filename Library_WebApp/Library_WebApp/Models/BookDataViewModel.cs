using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class BookDataViewModel
    {
        public Book book { get; set; }
        [Display(Name = "Gatunki")]
        public List<SelectListItem> Genres { get; set; }
        [Display(Name = "Autorzy")]
        [Required(ErrorMessage = "Trzeba wybrać conajmniej jednego autora")]
        public List<Author> Authors { get; set; }
        public BookDataViewModel()
        {
            book = new Book();
            Authors = new List<Author>();
        }


    }
}
