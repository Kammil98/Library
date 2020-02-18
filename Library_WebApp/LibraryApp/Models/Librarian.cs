using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models {
    public partial class Librarian {
        public string Login { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [DataType(DataType.Date)]
        [DateLessThanOrEqualToToday(ErrorMessage = "Data nie może przekraczać aktualnej daty")]
        public DateTime EmploymentDate { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public int BranchNumber { get; set; }

        public virtual Branch BranchNumberNavigation { get; set; }
        public virtual User LoginNavigation { get; set; }
    }
}
