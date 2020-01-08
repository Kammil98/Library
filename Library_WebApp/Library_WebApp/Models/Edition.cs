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
        public String title { set; get; }
        [DataType(DataType.Date)]
        public DateTime releaseDate { set; get; }
        public String publishingHouse { set; get; }
        public List<SelectListItem> Books { get; set; }
        public List<SelectListItem> PublishingHouses { get; set; }
    }
}
