using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Extensions;
using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DevAdventCalendarCompetition.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(IAccountRepository accountRepository,
            IEmailSender emailSender,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _accountRepository = accountRepository;
            _emailSender = emailSender;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
        }

        public async Task<ApplicationUser> GetTwoFactorAuthenticationUserAsync()
        {
            return await _signInManager.GetTwoFactorAuthenticationUserAsync();
        }

        public string GetUserId(ClaimsPrincipal principal)
        {
            return _userManager.GetUserId(principal);
        }

        public async Task<SignInResult> TwoFactorAuthenticatorSignInAsync(string authenticatorCode, bool rememberMe, bool rememberMachine)
        {
            return await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, rememberMachine);
        }

        public async Task<SignInResult> TwoFactorRecoveryCodeSignInAsync(string recoveryCode)
        {
            return await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);
        }

        public async Task<IdentityResult> CreateAsync(string email, string password)
        {
            var user = new ApplicationUser { UserName = email, Email = email };
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ClaimsPrincipal pricipal)
        {
            var user = await _userManager.GetUserAsync(pricipal);
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task SendEmailConfirmationAsync(string email, string callbackUrl)
        {
            await _emailSender.SendEmailConfirmationAsync(email, callbackUrl);
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await _emailSender.SendEmailAsync(email, subject, message);
        }

        public async Task SignInAsync(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);
            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
        {
            return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string userId = null)
        {
            return await _signInManager.GetExternalLoginInfoAsync(userId);
        }

        public async Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey)
        {
            return await _signInManager.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent: false, bypassTwoFactor: true);
        }

        public async Task<IdentityResult> AddLoginAsync(ClaimsPrincipal principal, ExternalLoginInfo info)
        {
            var user = await _userManager.GetUserAsync(principal);
            return await _userManager.AddLoginAsync(user, info);
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string code)
        {
            return await _userManager.ConfirmEmailAsync(user, code);
        }

        public async Task<bool> IsEmailConfirmedAsync(ApplicationUser user)
        {
            return await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string code, string password)
        {
            return await _userManager.ResetPasswordAsync(user, code, password);
        }
    }
}