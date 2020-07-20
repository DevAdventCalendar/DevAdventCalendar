using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IAccountService
    {
        Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe);

        string GetUserId(ClaimsPrincipal principal);

        ApplicationUser CreateApplicationUserByEmailAndUserName(string email, string userName);

        Task SignInWithApplicationUserAsync(ApplicationUser user);

        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);

        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);

        Task<string> GenerateEmailConfirmationTokenAsync(ClaimsPrincipal principal);

        Task SendEmailConfirmationAsync(string email, Uri callbackUrl, bool isNewEmail = false);

        Task SendEmailAsync(string email, string subject, string message);

        Task SignInAsync(ApplicationUser user);

        Task SignOutAsync();

        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, Uri redirectUrl);

        Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string userId = null);

        Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey);

        Task<IdentityResult> AddLoginAsync(ApplicationUser user, ExternalLoginInfo info);

        Task<ApplicationUser> FindByIdAsync(string userId);

        Task<ApplicationUser> FindByEmailAsync(string email);

        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string code);

        Task<bool> IsEmailConfirmedAsync(ApplicationUser user);

        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);

        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string code, string password);
    }
}