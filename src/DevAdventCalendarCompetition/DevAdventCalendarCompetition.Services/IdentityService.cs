using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace DevAdventCalendarCompetition.Services
{
    public class IdentityService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        public bool IsSignedIn(ClaimsPrincipal claimsPrincipal)
        {
            return this._signInManager.IsSignedIn(claimsPrincipal);
        }

        public string GetUserName(ClaimsPrincipal claimsPrincipal)
        {
            return this._userManager.GetUserName(claimsPrincipal);
        }

        public async Task<List<AuthenticationScheme>> GetExternalAuthenticationSchemes()
        {
            return (await this._signInManager.GetExternalAuthenticationSchemesAsync().ConfigureAwait(false)).ToList();
        }

        public bool IsUserLoggedIn(ClaimsPrincipal principal)
        {
            return this._signInManager.IsSignedIn(principal);
        }
    }
}