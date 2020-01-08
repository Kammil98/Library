using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Property
    {
        public String authorFirstName { set; get; }
        public String authorLastName { set; get; }
        public String title { set; get; }
        public List<SelectListItem> Authors { get; set; }
        public List<SelectListItem> Books { get; set; }
    }
}
