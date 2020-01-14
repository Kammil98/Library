using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class BorrowDataViewModel
    {
        public Borrow borrow { get; set; }
        public List<SelectListItem> Volumes { get; set; }

    }
}
