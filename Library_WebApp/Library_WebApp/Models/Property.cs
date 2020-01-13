using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Property
    {
        [Required]
        public int? authorId { set; get; }
        [Required]
        public int? BookId { set; get; }
    }
}
