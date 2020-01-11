using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Property
    {
        [Display(Name = "Imię autora")]
        public String authorFirstName { set; get; }
        [Display(Name = "Imię nazwisko autora")]
        public String authorLastName { set; get; }
        [Display(Name = "Tytuł")]
        public String title { set; get; }
        public List<SelectListItem> Authors { get; set; }
        public List<SelectListItem> Books { get; set; }
    }
}
