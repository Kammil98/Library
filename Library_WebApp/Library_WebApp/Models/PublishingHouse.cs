using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class PublishingHouse
    {
        public int id { set; get; }
        [Display(Name = "Wydawnictwo")]
        public String name { set; get; }
        public Address address { set; get; }
    }
}
