using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Reservation
    {
        public int id { set; get; }
        [Display(Name = "Data rezerwacji")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Pole {0} nie może być puste.")]
        public DateTime? reservationDate { set; get; }
        [Display(Name = "Id woluminu")]
        [Required(ErrorMessage = "Pole {0} nie może być puste.")]
        public int volumeId { set; get; }
        [Display(Name = "Login rezerwującego")]
        [Required(ErrorMessage = "Pole {0} nie może być puste.")]
        public String userLogin { set; get; }
}
}
