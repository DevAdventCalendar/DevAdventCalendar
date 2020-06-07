using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Pole Email jest obowiązkowe")]
        [EmailAddress(ErrorMessage = "Podaj prawidłowy format adresu email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole Hasło jest obowiązkowe")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętać mnie?")]
        public bool RememberMe { get; set; }
    }
}