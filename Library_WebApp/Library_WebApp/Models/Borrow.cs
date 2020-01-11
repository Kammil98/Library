using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Borrow
    {
        public int id { set; get; }
        [DataType(DataType.Date)]
        [Display(Name = "Data wypożyczenia")]
        public DateTime borrowDate { set; get; }
        [Display(Name = "Data zwrotu")]
        [DataType(DataType.Date)] 
        public DateTime returnDate { set; get; }
        [Display(Name = "Id woluminu")]
        public int volumeId { set; get; }
        [Display(Name = "Login wypożyczającego")]
        public String userLogin { set; get; }
    }
}
