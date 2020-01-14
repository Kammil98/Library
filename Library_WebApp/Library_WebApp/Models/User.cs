using System.ComponentModel.DataAnnotations;

namespace Library_WebApp.Models
{
    public class User
    {
        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Pole {0} nie może być puste.")]
        public string name { set; get; }
        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Pole {0} nie może być puste.")]
        public string lastName { set; get; }

        //[Required(ErrorMessage = "Pole data jest obowiązkowe.")]
        //[RegularExpression(@"^(3[0-1]|[1-2][0-9]|[1-9])-(1[0-2]|[1-9])-[1-2][0-9][0-9][0-9]$", ErrorMessage = "Pole data musi mieć strukturę dd-mm-rrrr")]
        //public string data { set; get; }
        [Display(Name = "Login")]
        [StringLength(20, ErrorMessage = "login musi mieć od {2} do {1} znaków.", MinimumLength = 3)]
        [Required(ErrorMessage = "Pole {0} nie może być puste.")]
        public string login { set; get; }
        [Display(Name = "Hasło")]
        [StringLength(20, ErrorMessage = "hasło musi mieć od {2} do {1} znaków.", MinimumLength = 5)]
        [Required(ErrorMessage = "Pole {0} nie może być puste.")]
        public string password { set; get; }
        [Display(Name = "Powtórz hasło")]
        [Compare("password", ErrorMessage = "Wprowadzane hasła są różne. Proszę powtórzyć hasło wpisane w pole hasło.")]
        public string passwordRepeat { set; get; }
    }
}
