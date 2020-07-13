using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Extensions;
using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace DevAdventCalendarCompetition.Services
{
    public class AccountService : IAccountService
    {
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(
            IEmailSender emailSender,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this._emailSender = emailSender;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe)
        {
            return await this._signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false).ConfigureAwait(false);
        }

        public string GetUserId(ClaimsPrincipal principal)
        {
            return this._userManager.GetUserId(principal);
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return await this._userManager.CreateAsync(user).ConfigureAwait(false);
            }
            else
            {
                return await this._userManager.CreateAsync(user, password).ConfigureAwait(false);
            }
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            return await this._userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ClaimsPrincipal principal)
        {
            var user = await this._userManager.GetUserAsync(principal).ConfigureAwait(false);
            return await this._userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
        }

        public async Task SendEmailConfirmationAsync(string email, string callbackUrl, bool isNewEmail)
        {
            await this._emailSender.SendEmailConfirmationAsync(email, callbackUrl, isNewEmail).ConfigureAwait(false);
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await this._emailSender.SendEmailAsync(email, subject, message).ConfigureAwait(false);
        }

        public ApplicationUser CreateApplicationUserByEmail(string email)
        {
            return new ApplicationUser { UserName = email, Email = email };
        }

        public async Task SignInWithApplicationUserAsync(ApplicationUser user)
        {
            await this._signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);
        }

        public async Task SignInAsync(ApplicationUser user)
        {
            await this._signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);
        }

        public async Task SignOutAsync()
        {
            await this._signInManager.SignOutAsync().ConfigureAwait(false);
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
        {
            return this._signInManager.ConfigureExternalAuthenticationProperties(
                provider,
                redirectUrl);
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string userId = null)
        {
            return await this._signInManager.GetExternalLoginInfoAsync(userId).ConfigureAwait(false);
        }

        public async Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey)
        {
            return await this._signInManager.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent: false, bypassTwoFactor: true).ConfigureAwait(false);
        }

        public async Task<IdentityResult> AddLoginAsync(ApplicationUser user, ExternalLoginInfo info)
        {
            return await this._userManager.AddLoginAsync(user, info).ConfigureAwait(false);
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return await this._userManager.FindByIdAsync(userId).ConfigureAwait(false);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await this._userManager.FindByEmailAsync(email).ConfigureAwait(false);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string code)
        {
            return await this._userManager.ConfirmEmailAsync(user, code).ConfigureAwait(false);
        }

        public async Task<bool> IsEmailConfirmedAsync(ApplicationUser user)
        {
            return await this._userManager.IsEmailConfirmedAsync(user).ConfigureAwait(false);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return await this._userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string code, string password)
        {
            return await this._userManager.ResetPasswordAsync(user, code, password).ConfigureAwait(false);
        }

        public async Task SendEmailConfirmationAsync(string email, Uri callbackUrl, bool isNewEmail)
        {
            if (callbackUrl is null)
            {
                throw new ArgumentNullException(nameof(callbackUrl));
            }

            await this._emailSender.SendEmailConfirmationAsync(email, callbackUrl.ToString(), isNewEmail).ConfigureAwait(false);
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, Uri redirectUrl)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            if (redirectUrl is null)
            {
                throw new ArgumentNullException(nameof(redirectUrl));
            }

            return this._signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl.ToString());
        }
    }
}