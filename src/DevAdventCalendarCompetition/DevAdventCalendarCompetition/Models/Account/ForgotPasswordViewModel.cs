using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Pole Email jest obowiązkowe")]
        [EmailAddress(ErrorMessage = "Podaj prawidłowy format adresu email")]
        public string Email { get; set; }
    }
}