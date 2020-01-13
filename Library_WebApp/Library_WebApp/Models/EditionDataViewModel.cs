using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class EditionDataViewModel
    {
        public Edition edition { get; set; }
        public List<SelectListItem> Books { get; set; }
        public List<SelectListItem> PublishingHouses { get; set; }
        public EditionDataViewModel()
        {
            edition = new Edition();
        }
    }
}
