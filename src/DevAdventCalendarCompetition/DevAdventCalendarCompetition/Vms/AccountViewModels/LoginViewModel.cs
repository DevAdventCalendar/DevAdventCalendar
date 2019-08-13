using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Vms
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Pole Email jest obowiązkowe")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Podaj prawidłowy format adresu email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole Hasło jest obowiązkowe")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj mnie?")]
        public bool RememberMe { get; set; }
    }
}