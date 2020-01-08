using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Borrow
    {
        [DataType(DataType.Date)]
        public DateTime borrowDate { set; get; }
        [DataType(DataType.Date)] 
        public DateTime returnDate { set; get; }
        public int volumeId { set; get; }
        public String userLogin { set; get; }
    }
}
