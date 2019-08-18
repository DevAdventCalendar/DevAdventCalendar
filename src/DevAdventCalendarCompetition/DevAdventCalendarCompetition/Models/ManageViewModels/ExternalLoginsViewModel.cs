using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace DevAdventCalendarCompetition.Models.ManageViewModels
{
    public class ExternalLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; private set; }

        public IList<AuthenticationScheme> OtherLogins { get; private set; }

        public bool ShowRemoveButton { get; set; }

        public string StatusMessage { get; set; }
    }
}