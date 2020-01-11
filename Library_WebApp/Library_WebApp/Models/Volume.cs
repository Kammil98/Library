using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Volume
    {
        public enum BookState {
            [Description("Dostępna")]
            Aviable,
            [Description("Zarezerwowana")]
            Reserved,
            [Description("Wypożyczona")]
            Borrowed 
        };
        public int id { set; get; }
        [Display(Name = "Id wydania")]
        public int editionId { set; get; }
        [Display(Name = "Nr filii")]
        public int libraryBranchId { set; get; }
        [Display(Name = "Stan")]
        public String condition { set; get; }
        [Display(Name = "Dostępność")]
        public BookState State { set; get; }
        public List<SelectListItem> Editions { get; set; }
        public List<SelectListItem> LibraryBranchIds { get; set; }
    }
}
