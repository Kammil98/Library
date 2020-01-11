using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Address
    {
        public int id { set; get; }
        [Display(Name = "Ulica")]
        public String street { set; get; }
        [Display(Name = "Numer")]
        public int buildingNr { set; get; }
        [Display(Name = "Kraj")]
        public String Country { set; get; }
        [Display(Name = "Miasto")]
        public String City { set; get; }
        [Display(Name = "Kod pocztowy")]
        public String zipCode { set; get; }
    }
}
