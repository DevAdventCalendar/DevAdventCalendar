using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Models.Account
{
    public class ExternalLoginViewModel
    {
        [Required(ErrorMessage = "Pole nazwa użytkownika jest obowiązkowe")]
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Pole Email jest obowiązkowe")]
        [EmailAddress(ErrorMessage = "Podaj prawidłowy format adresu email")]
        public string Email { get; set; }
    }
}