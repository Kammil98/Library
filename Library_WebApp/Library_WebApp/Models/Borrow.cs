using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Borrow
    {
        public DateTime borrowDate { set; get; }
        public DateTime returnDate { set; get; }
        public int volumeId { set; get; }
        public String userLogin { set; get; }
    }
}
