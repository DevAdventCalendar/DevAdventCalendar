using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevAdventCalendarCompetition.Vms
{
    public class AddPhoneNumberViewModel
    {
        [Required(ErrorMessage = "Pole Numer telefonu jest obowiązkowe")]
        [Phone(ErrorMessage = "Podaj prawidłowy format numeru telefonu")]
        [Display(Name = "Numer telefonu")]
        public string Number { get; set; }
    }
}