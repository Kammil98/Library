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
        public BookDataViewModel()
        {
            book = new Book();
        }


    }
}
