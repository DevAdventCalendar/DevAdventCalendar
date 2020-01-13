using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevAdventCalendarCompetition.Vms
{
    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; private set; }

        // TODO:  AuthenticationDescription applies only .Net Core 2.2 2.1 2.0 1.1 1.0
        public IList<AuthenticationDescription> OtherLogins { get; private set; }

        public IList<AuthenticationScheme> ExternalProviders { get; private set; }
    }
}