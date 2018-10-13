using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DevAdventCalendarCompetition.Services
{
    public class IdentityService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public bool IsSignedIn(ClaimsPrincipal claimsPrincipal)
        {
            return _signInManager.IsSignedIn(claimsPrincipal);
        }

        public string GetUserName(ClaimsPrincipal claimsPrincipal)
        {
            return _userManager.GetUserName(claimsPrincipal);
        }

        public async Task<List<AuthenticationScheme>> GetExternalAuthenticationSchemes()
        {
            return (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public bool IsUserLoggedIn(ClaimsPrincipal principal)
        {
            return _signInManager.IsSignedIn(principal);
        }
    }
}