using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            Borrowed };
        public int id { set; get; }
        public int editionId { set; get; }
        public int libraryBranchId { set; get; }
        public String condition { set; get; }
        public BookState State { set; get; }
        public List<SelectListItem> Editions { get; set; }
        public List<SelectListItem> LibraryBranchIds { get; set; }
    }
}
