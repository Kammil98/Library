using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class BooksList
    {
        public string SearchLibraryBranchId { get; set; }
        public string SearchName { get; set; }
        public string SearchPublishingHouse { get; set; }
        public string SearchAuthor { get; set; }
        public string SearchGenre { get; set; }
        public Volume volume { get; set; }//choosed Volume
        public List<Book> Books { get; set; }//keep all books
        public List<Volume> Volumes { get; set; }//keep volumes of choosen book
        public List<SelectListItem> Genres { get; set; }//keep all aviable genres
        public List<SelectListItem> LibraryBranchIds { get; set; }//keep all aviable LibraryBranchIds

        public BooksList()
        {
            Books = new List<Book>();
            Genres = new List<SelectListItem>();
            LibraryBranchIds = new List<SelectListItem>();
            Volumes = new List<Volume>();
        }
    }
}
