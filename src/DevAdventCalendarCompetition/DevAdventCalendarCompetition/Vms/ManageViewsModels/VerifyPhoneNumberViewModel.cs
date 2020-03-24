using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevAdventCalendarCompetition.Vms
{
    public class VerifyPhoneNumberViewModel
    {
        [Required(ErrorMessage = "Pole Kod jest obowiązkowe")]
        [Display(Name = "Kod")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Pole Numer telefonu jest obowiązkowe")]
        [Phone(ErrorMessage = "Podaj prawidłowy format numeru telefonu")]
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }
    }
}