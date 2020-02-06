using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Models {
    public class PublishingHousesViewModel {
        public IEnumerable<PublishingHouse> PublishingHouses { get; set; }
        public PublishingHouse Selection { get; set; }
        public IEnumerable<Edition> Editions { get; set; }

        [Display(Name = "Nazwa")]
        public string NameFilter { get; set; }
        [Display(Name = "Adres")]
        public string AddressFilter { get; set; }
    }
}
