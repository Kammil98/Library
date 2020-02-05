using System;
using System.Collections.Generic;

namespace LibraryApp.Models
{
    public partial class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual PublishingHouse PublishingHouse { get; set; }
    }
}
