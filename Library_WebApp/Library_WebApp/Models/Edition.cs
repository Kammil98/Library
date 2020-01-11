using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Edition
    {
        int id { set; get; }
        [Display(Name = "Tytuł")]
        public String title { set; get; }//title of a book
        [Display(Name = "Data wydania")]
        [DataType(DataType.Date)]
        public DateTime releaseDate { set; get; }
        [Display(Name = "Wydawnictwo")]
        public String publishingHouse { set; get; }
        public List<SelectListItem> Books { get; set; }
        public List<SelectListItem> PublishingHouses { get; set; }
    }
}
