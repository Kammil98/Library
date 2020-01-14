using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Property
    {
        public int id { set; get; }
        [Required(ErrorMessage = "Pole Autor nie może być puste.")]
        public int authorId { set; get; }
        [Required(ErrorMessage = "Pole Książka nie może być puste.")]
        public int BookId { set; get; }
    }
}
