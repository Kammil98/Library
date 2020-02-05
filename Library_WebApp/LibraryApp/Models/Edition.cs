using System;
using System.Collections.Generic;

namespace LibraryApp.Models
{
    public partial class Edition
    {
        public Edition()
        {
            BookCopy = new HashSet<BookCopy>();
        }

        public int Id { get; set; }
        public int BookId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string PublishingHouse { get; set; }

        public Book IdNavigation { get; set; }
        public PublishingHouse PublishingHouseNavigation { get; set; }
        public ICollection<BookCopy> BookCopy { get; set; }
    }
}
