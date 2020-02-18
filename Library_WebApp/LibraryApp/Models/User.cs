using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models {
    public partial class User {
        [Required(ErrorMessage = "To pole jest wymagane")]
        [StringLength(64, ErrorMessage = "Login musi mieć od {2} do {1} znaków.", MinimumLength = 4)]
        [RegularExpression(@"[^ \t]*", ErrorMessage = "Login nie może zawierać białych znaków")]
        public string Login { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "To pole jest wymagane")]
        [StringLength(64, ErrorMessage = "Hasło musi mieć od {2} do {1} znaków", MinimumLength = 8)]
        public string Password { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression(@"^[A-Z][a-zA-Z \-]*$", ErrorMessage = "Imię zawiera niepoprawne znaki")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression(@"^[A-Z][a-zA-Z \-]*$", ErrorMessage = "Nazwisko zawiera niepoprawne znaki")]
        public string LastName { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Compare("Password", ErrorMessage = "Podane hasła nie są takie same")]
        public string ConfirmPassword { get; set; }

        public virtual Librarian Librarian { get; set; }
        public virtual Reader Reader { get; set; }
    }
}
