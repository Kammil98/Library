using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class publishingHouseListViewModel
    {
        [Display(Name = "Nazwa")]
        public string SearchLibraryName { get; set; }
        [Display(Name = "Ulica")]
        public string SearchLibraryStreet { get; set; }
        [Display(Name = "Miasto")]
        public string SearchLibraryCity { get; set; }
        [Display(Name = "Kraj")]
        public string SearchLibraryCountry { get; set; }
        public PublishingHouse publishingHouse { get; set; }
        public List<PublishingHouse> PublishingHouses { get; set; }
        public List<Edition> Editions { get; set; }
        public List<Book> Books { get; set; }
        public publishingHouseListViewModel()
        {
            PublishingHouses = new List<PublishingHouse>();
            Editions = new List<Edition>();
            Books = new List<Book>();
        }
    }
}
