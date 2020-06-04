using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevAdventCalendarCompetition.Models.AccountViewModels
{
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }

        public ICollection<SelectListItem> Providers { get; private set; }

        public Uri ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }
}