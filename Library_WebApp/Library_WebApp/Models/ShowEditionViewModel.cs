using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class ShowEditionViewModel
    {
        public Edition edition { get; set; }
        public Book book { get; set; }
        [Display(Name = "Wydawnictwo")]
        public string publishingHouseName { get; set; }
        public ShowEditionViewModel()
        {
            edition = new Edition();
        }
    }
}
