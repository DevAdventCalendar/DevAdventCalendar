using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Vms
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole Hasło jest obowiązkowe")]
        [StringLength(100, ErrorMessage = "Długość {0} powinna być większa niż {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasła nie pasują do siebie")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}