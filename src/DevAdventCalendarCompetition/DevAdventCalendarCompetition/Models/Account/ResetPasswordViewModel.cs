using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Models.Account
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Pole Email jest obowiązkowe")]
        [EmailAddress(ErrorMessage = "Wprowadź prawidłowy format adresu email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole Hasło jest obowiązkowe")]
        [StringLength(100, ErrorMessage = "Długość {0} powinna być większa niż {2} i mniejsza niż {1}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasło i hasło potwierdzające nie pasują do siebie.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}