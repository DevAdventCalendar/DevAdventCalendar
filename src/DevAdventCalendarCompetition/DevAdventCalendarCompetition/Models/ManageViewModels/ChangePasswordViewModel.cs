using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Models.ManageViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Pole Bieżące hasło jest obowiązkowe")]
        [DataType(DataType.Password)]
        [Display(Name = "Bieżące hasło")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Pole Nowe hasło jest obowiązkowe")]
        [StringLength(100, ErrorMessage = "Długość hasła w polu {0} powinna być większa niż {2} i mniejsza niż {1}", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Hasła nie pasują do siebie")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}