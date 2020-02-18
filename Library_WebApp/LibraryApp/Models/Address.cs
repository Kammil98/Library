using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models {
    public partial class Address {
        public int Id { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression(@"^[a-zA-Z0-9][a-zA-Z0-9 \-]*$", ErrorMessage = "Nazwa ulicy zawiera niepoprawne znaki")]
        public string Street { get; set; }
        [RegularExpression(@"^[A-Z][a-zA-Z \-]*$", ErrorMessage = "Nazwa kraju zawiera niepoprawne znaki")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Country { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression(@"^[A-Z][a-zA-Z \-]*$", ErrorMessage = "Nazwa miasta zawiera niepoprawne znaki")]
        public string City { get; set; }
        public string ZipCode { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual PublishingHouse PublishingHouse { get; set; }
    }
}
