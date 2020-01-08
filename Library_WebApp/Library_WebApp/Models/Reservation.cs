using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Reservation
    {
        [DataType(DataType.Date)]
        public DateTime reservationDate { set; get; }
        public int volumeId { set; get; }
        public String userLogin { set; get; }
}
}
