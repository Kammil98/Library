using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Edition
    {
        public int id { set; get; }
        [Display(Name = "Id książki")]
        public int bookId { set; get; }
        [Display(Name = "Data wydania")]
        [DataType(DataType.Date)]
        public DateTime releaseDate { set; get; }
        [Display(Name = "Id wydawnictwa")]
        public int publishingHouseId { set; get; }
    }
}
