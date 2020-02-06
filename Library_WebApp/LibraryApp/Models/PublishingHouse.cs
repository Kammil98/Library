using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public partial class PublishingHouse
    {
        public PublishingHouse()
        {
            Edition = new HashSet<Edition>();
        }

        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Name { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Edition> Edition { get; set; }
    }
}
