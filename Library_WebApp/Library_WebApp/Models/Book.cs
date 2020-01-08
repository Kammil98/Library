using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Book
    {
        public String name { set; get; }
        public String genre { set; get; }
        public List<SelectListItem> Genres { get; set; }
    }
}
