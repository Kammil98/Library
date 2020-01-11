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
        public DateTime reservationDate { set; get; }
        [Display(Name = "id woluminu")]
        public int volumeId { set; get; }
        [Display(Name = "login rezerwującego")]
        public String userLogin { set; get; }
}
}
