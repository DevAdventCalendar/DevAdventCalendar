using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Models.AccountViewModels
{
    public class LoginWith2faViewModel
    {
        [Required(ErrorMessage = "Pole Kod uwierzytelniający jest obowiązkowe")]
        [StringLength(7, ErrorMessage = "Długość {0} powinna być większa niż {2} i mniejsza niż {1}.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Kod uwierzytelniający")]
        public string TwoFactorCode { get; set; }

        [Display(Name = "Zapamiętaj urządzenie")]
        public bool RememberMachine { get; set; }

        [Display(Name = "Zapamiętaj mnie")]
        public bool RememberMe { get; set; }
    }
}