﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Volume
    {
        public int id { set; get; }
        public int editionId { set; get; }
        public int libraryBranchId { set; get; }
        public String condition { set; get; }
        public List<SelectListItem> Editions { get; set; }
        public List<SelectListItem> LibraryBranchIds { get; set; }
    }
}
