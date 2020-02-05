using System;
using System.Collections.Generic;

namespace LibraryApp.Models
{
    public partial class Address
    {
        public Address()
        {
            Branch = new HashSet<Branch>();
            PublishingHouse = new HashSet<PublishingHouse>();
        }

        public int Id { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        public ICollection<Branch> Branch { get; set; }
        public ICollection<PublishingHouse> PublishingHouse { get; set; }
    }
}
