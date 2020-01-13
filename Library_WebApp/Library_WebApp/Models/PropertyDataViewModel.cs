using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class PropertyDataViewModel
    {
        public Property property { get; set; }
        public List<SelectListItem> Authors { get; set; }
        public List<SelectListItem> Books { get; set; }
        public PropertyDataViewModel()
        {
            property = new Property();
        }
    }
}
