using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Address
    {
        public int id { set; get; }
        public String street { set; get; }
        public int buildingNr { set; get; }
        public String Country { set; get; }
        public String City { set; get; }
        public String zipCode { set; get; }
    }
}
