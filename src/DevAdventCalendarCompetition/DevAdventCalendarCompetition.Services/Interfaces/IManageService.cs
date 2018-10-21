using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IManageService
    {
        Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal);

        Task<bool> HasPasswordAsync(ApplicationUser user);

        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword);

        Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user);

        Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();

        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId);

        Task<IdentityResult> SetEmailAsync(ApplicationUser user, string email);

        Task<IdentityResult> SetPhoneNumberAsync(ApplicationUser user, string phoneNumber);

        Task<IdentityResult> AddPasswordAsync(ApplicationUser user, string password);

        Task<IdentityResult> RemoveLoginAsync(ApplicationUser user, string loginProvider, string providerKey);

        Task<string> GetAuthenticatorKeyAsync(ApplicationUser user);

        Task<int> CountRecoveryCodesAsync(ApplicationUser user);

        Task<IdentityResult> SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled = false);

        Task<bool> VerifyTwoFactorTokenAsync(ApplicationUser user, string code);

        Task<IEnumerable<string>> GenerateNewTwoFactorRecoveryCodesAsync(ApplicationUser user);

        Task<IdentityResult> ResetAuthenticatorKeyAsync(ApplicationUser user);
    }
}