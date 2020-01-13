using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class VolumeDataViewModel
    {
        public Volume volume { set; get; }
        public List<SelectListItem> Editions { get; set; }
        public List<SelectListItem> LibraryBranchIds { get; set; }
        public VolumeDataViewModel()
        {
            volume = new Volume();
        }
    }
}
