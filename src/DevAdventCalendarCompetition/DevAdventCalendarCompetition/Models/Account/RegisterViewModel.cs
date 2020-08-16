using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Pole nazwa użytkownika jest obowiązkowe")]
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Pole Email jest obowiązkowe")]
        [EmailAddress(ErrorMessage = "Podaj prawidłowy format adresu email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole Hasło jest obowiązkowe")]
        [StringLength(100, ErrorMessage = "Długość {0} powinna być większa niż {2} i mniejsza niż {1}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Hasło powinno: zawierać wielkie, małe litery, cyfry i znaki specjalne.")]
        [Display(Name = "hasła")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Hasło potwierdzające")]
        [Compare("Password", ErrorMessage = "Wprowadzone hasło i hasło potwierdzające nie są zgodne.")]
        public string ConfirmPassword { get; set; }
    }
}