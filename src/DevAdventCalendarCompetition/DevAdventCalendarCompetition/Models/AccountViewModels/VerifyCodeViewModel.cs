using System;
using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Models.AccountViewModels
{
    public class VerifyCodeViewModel
    {
        [Required(ErrorMessage = "Pole Provider jest obowiązkowe")]
        public string Provider { get; set; }

        [Required(ErrorMessage = "Pole Kod jest obowiązkowe")]
        [Display(Name = "Kod")]
        public string Code { get; set; }

        public Uri ReturnUrl { get; set; }

        [Display(Name = "Zapamiętać przeglądarkę?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }
}