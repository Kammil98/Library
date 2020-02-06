using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models {
    public partial class Address {
        public int Id { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Street { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Country { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string City { get; set; }
        public string ZipCode { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual PublishingHouse PublishingHouse { get; set; }
    }
}
