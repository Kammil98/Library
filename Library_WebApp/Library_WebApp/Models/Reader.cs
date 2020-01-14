using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Reader : User
    {
        [Display(Name = "Data urodzenia")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Pole {0} nie może być puste.")]
        public DateTime dateOfBirth { set; get; }
    }
}
