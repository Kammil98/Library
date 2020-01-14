using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class ShowVolumeViewModel
    {
        public Volume volume { get; set; }
        public Book book { get; set; }
        public Edition edition { get; set; }
        [Display(Name = "Wydawnictwo")]
        public string publishingHouseName { get; set; }
        public ShowVolumeViewModel()
        {
            book = new Book();
            volume = new Volume();
            edition = new Edition();
        }
    }
}
