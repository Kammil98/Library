using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class BooksListViewModel
    {
        [Display(Name = "Nr filii")]
        public string SearchLibraryBranchId { get; set; }
        [Display(Name = "Tytuł")]
        public string SearchName { get; set; }
        [Display(Name = "Wydawnictwo")]
        public string SearchPublishingHouse { get; set; }
        [Display(Name = "Autor")]
        public string SearchAuthor { get; set; }
        [Display(Name = "Gatunek")]
        public string SearchGenre { get; set; }
        [Display(Name = "Wypożyczone")]
        public Boolean isBorrowed { get; set; }
        [Display(Name = "Zarezerwowane")]
        public Boolean isReserved { get; set; }
        [Display(Name = "Dostępne")]
        public Boolean isAviable { get; set; }
        public Volume volume { get; set; }//choosed Volume
        public List<Book> Books { get; set; }//keep all books
        public List<Volume> Volumes { get; set; }//keep volumes of choosen book
        public List<PublishingHouse> PublishingHouses { get; set; }//keep PublishingHouses of all books, to search its names to display
        public List<SelectListItem> Genres { get; set; }//keep all aviable genres
        public List<SelectListItem> LibraryBranchIds { get; set; }//keep all aviable LibraryBranchIds

        public BooksListViewModel()
        {
            Books = new List<Book>();
            Genres = new List<SelectListItem>();
            LibraryBranchIds = new List<SelectListItem>();
            PublishingHouses = new List<PublishingHouse>();
        }
    }
}
