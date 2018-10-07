using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IAccountService
    {
        Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe);

        Task<ApplicationUser> GetTwoFactorAuthenticationUserAsync();

        string GetUserId(ClaimsPrincipal pricipal);

        Task<SignInResult> TwoFactorAuthenticatorSignInAsync(string authenticatorCode, bool rememberMe, bool rememberMachine);

        Task<SignInResult> TwoFactorRecoveryCodeSignInAsync(string recoveryCode);

        Task<IdentityResult> CreateAsync(string email, string password);

        Task<string> GenerateEmailConfirmationTokenAsync(ClaimsPrincipal principal);

        Task SendEmailConfirmationAsync(string email, string callbackUrl);

        Task SendEmailAsync(string email, string subject, string message);

        Task SignInAsync(ClaimsPrincipal principal);

        Task SignOutAsync();

        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl);

        Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string userId = null);

        Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey);

        Task<IdentityResult> AddLoginAsync(ClaimsPrincipal principal, ExternalLoginInfo info);

        Task<ApplicationUser> FindByIdAsync(string userId);

        Task<ApplicationUser> FindByEmailAsync(string email);

        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string code);

        Task<bool> IsEmailConfirmedAsync(ApplicationUser user);

        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);

        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string code, string password);
    }
}