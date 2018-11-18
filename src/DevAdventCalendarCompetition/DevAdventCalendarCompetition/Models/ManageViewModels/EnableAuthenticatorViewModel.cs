using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Models.ManageViewModels
{
    public class EnableAuthenticatorViewModel
    {
        [Required(ErrorMessage = "Pole Kod weryfikacyjny jest obowiązkowe")]
        [StringLength(7, ErrorMessage = "Długość {0} powinna być większa niż {2} i mniejsza niż {1}", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Kod weryfikacyjny")]
        public string Code { get; set; }

        [BindNever]
        public string SharedKey { get; set; }

        [BindNever]
        public string AuthenticatorUri { get; set; }
    }
}