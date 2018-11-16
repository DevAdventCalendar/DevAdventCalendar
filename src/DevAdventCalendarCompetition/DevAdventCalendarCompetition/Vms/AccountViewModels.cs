using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Vms
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "Pole Email jest obowiązkowe")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required(ErrorMessage = "Pole Provider jest obowiązkowe")]
        public string Provider { get; set; }

        [Required(ErrorMessage = "Pole Kod jest obowiązkowe")]
        [Display(Name = "Kod")]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Zapamiętać przeglądarkę?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Pole Email jest obowiązkowe")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

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

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Pole Email jest obowiązkowe")]
        [EmailAddress(ErrorMessage = "Podaj poprawny format email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole Imię jest obowiązkowe")]
        [StringLength(100, ErrorMessage = "Pole Imię może mieć maksymalnie 100 znaków")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Pole Nazwisko jest obowiązkowe")]
        [StringLength(100, ErrorMessage = "Pole Nazwisko może mieć maksymalnie 100 znaków")]
        [Display(Name = "Nazwisko")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Pole Telefon jest obowiązkowe")]
        [StringLength(100, ErrorMessage = "Pole Telefon może mieć maksymalnie 100 znaków")]
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Pole Hasło jest obowiązkowe")]
        [StringLength(100, ErrorMessage = "Długość {0} powinna być większa niż {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Pole Hasło potwierdzające jest obowiązkowe")]
        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasło i hasło potwierdzające nie pasują do siebie.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Pole Email jest obowiązkowe")]
        [EmailAddress(ErrorMessage = "Podaj prawidłowy format adresu email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole Hasło jest obowiązkowe")]
        [StringLength(100, ErrorMessage = "Długość {0} powinna być większa niż {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Hasło potwierdzające")]
        [Compare("Password", ErrorMessage = "Hasło i hasło potwierdzające nie pasują do siebie")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Pole Email jest obowiązkowe")]
        [EmailAddress(ErrorMessage = "Podaj prawidłowy format adresu email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}