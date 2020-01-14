using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    [Display(Name = "Adres")]
    public class Address
    {
        public int id { set; get; }
        [Display(Name = "Ulica")]
        [Required(ErrorMessage = "Pole {0} nie może być puste.")]
        public String street { set; get; }
        [Display(Name = "Nr budynku")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} musi być z zakresu od {1} do {2}.")]
        [Required(ErrorMessage = "Pole {0} nie może być puste.")]
        public int? buildingNr { set; get; }
        [Display(Name = "Kraj")]
        [Required(ErrorMessage = "Pole {0} nie może być puste.")]
        public String Country { set; get; }
        [Display(Name = "Miejscowość")]
        [Required(ErrorMessage = "Pole {0} nie może być puste.")]
        public String City { set; get; }
        [Display(Name = "Kod pocztowy")]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "{0} musi być w formacie XX-XXX")]
        public String zipCode { set; get; }
    }
}
