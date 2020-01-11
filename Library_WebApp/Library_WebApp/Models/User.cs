using System.ComponentModel.DataAnnotations;

namespace Library_WebApp.Models
{
    public class User
    {
        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Pole imię jest obowiązkowe.")]
        public string name { set; get; }
        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Pole nazwisko jest obowiązkowe.")]
        public string lastName { set; get; }

        //[Required(ErrorMessage = "Pole data jest obowiązkowe.")]
        //[RegularExpression(@"^(3[0-1]|[1-2][0-9]|[1-9])-(1[0-2]|[1-9])-[1-2][0-9][0-9][0-9]$", ErrorMessage = "Pole data musi mieć strukturę dd-mm-rrrr")]
        //public string data { set; get; }
        [Display(Name = "Login")]
        [Required(ErrorMessage = "Pole login jest obowiązkowe.")]
        [StringLength(20, ErrorMessage = "login musi mieć od {2} do {1} znaków.", MinimumLength = 3)]
        public string login { set; get; }
        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Pole hasło jest obowiązkowe.")]
        [StringLength(20, ErrorMessage = "hasło musi mieć od {2} do {1} znaków.", MinimumLength = 5)]
        public string password { set; get; }
        [Display(Name = "Powtórz hasło")]
        [Compare("password", ErrorMessage = "Wprowadzane hasła są różne. Proszę powtórzyć hasło wpisane w pole hasło.")]
        public string passwordRepeat { set; get; }
    }
}
