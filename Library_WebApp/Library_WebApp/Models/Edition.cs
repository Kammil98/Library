using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Edition
    {
        int id { set; get; }
        public String title { set; get; }
        public DateTime releaseDate { set; get; }
        public String publishingHouse { set; get; }
    }
}
