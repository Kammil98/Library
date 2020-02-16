using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models {
    public partial class Edition {
        public Edition() {
            BookCopy = new HashSet<BookCopy>();
        }

        public int Id { get; set; }
        public int BookId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string PublishingHouse { get; set; }

        public virtual Book Book { get; set; }
        public virtual PublishingHouse PublishingHouseNavigation { get; set; }
        public virtual ICollection<BookCopy> BookCopy { get; set; }

        [NotMapped]
        public virtual string EditionString {
            get {
                return $"{PublishingHouse} ({ReleaseDate.ToString("d")})";
            }
        }
    }
}
