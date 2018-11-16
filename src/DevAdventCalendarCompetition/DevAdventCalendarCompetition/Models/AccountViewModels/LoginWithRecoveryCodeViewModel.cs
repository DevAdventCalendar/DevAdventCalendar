using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Models.AccountViewModels
{
    public class LoginWithRecoveryCodeViewModel
    {
        [Required(ErrorMessage = "Pole Kod odzyskiwania jest obowiązkowe")]
        [DataType(DataType.Text)]
        [Display(Name = "Kod odzyskiwania")]
        public string RecoveryCode { get; set; }
    }
}