using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Vms
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
        public IList<AuthenticationScheme> ExternalProviders { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required(ErrorMessage = "Pole Nowe hasło jest obowiązkowe")]
        [StringLength(100, ErrorMessage = "Długość {0} powinna być większa niż {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Nowe hasło i hasło potwierdzające nie pasują do siebie.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Pole Bieżące hasło jest obowiązkowe")]
        [DataType(DataType.Password)]
        [Display(Name = "Bieżące hasło")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Pole Nowe hasło jest obowiązkowe")]
        [StringLength(100, ErrorMessage = "Długość {0} powinna być większa niż {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Nowe hasło i hasło potwierdzające nie pasują do siebie.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required(ErrorMessage = "Pole Numer telefonu jest obowiązkowe")]
        [Phone(ErrorMessage = "Podaj prawidłowy format numeru telefonu")]
        [Display(Name = "Numer telefonu")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required(ErrorMessage = "Pole Kod jest obowiązkowe")]
        [Display(Name = "Kod")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Pole Numer telefonu jest obowiązkowe")]
        [Phone(ErrorMessage = "Podaj prawidłowy format numeru telefonu")]
        [Display(Name = "Number telefonu")]
        public string PhoneNumber { get; set; }
    }
}