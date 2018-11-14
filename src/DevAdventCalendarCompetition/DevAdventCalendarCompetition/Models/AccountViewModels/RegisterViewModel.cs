using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Pole Email jest obowiązkowe")]
        [EmailAddress(ErrorMessage = "Podaj prawidłowy format adresu email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole Hasło jest obowiązkowe")]
        [StringLength(100, ErrorMessage = "Długość {0} powinna być większa niż {2} i mniejsza niż {1}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "hasła")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Hasło potwierdzające")]
        [Compare("Password", ErrorMessage = "Wprowadzone hasło i hasło potwierdzające nie są zgodne.")]
        public string ConfirmPassword { get; set; }
    }
}