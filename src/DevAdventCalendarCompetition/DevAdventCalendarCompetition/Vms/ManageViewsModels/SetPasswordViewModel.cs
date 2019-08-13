using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevAdventCalendarCompetition.Vms
{

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
}