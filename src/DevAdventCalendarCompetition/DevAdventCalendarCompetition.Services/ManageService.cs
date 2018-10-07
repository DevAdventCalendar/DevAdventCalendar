using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DevAdventCalendarCompetition.Services
{
    public class ManageService : IManageService
    {
        private readonly IManageRepository _manageRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public ManageService(IManageRepository manageRepository,
            UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          IEmailSender emailSender)
        {
            _manageRepository = manageRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal)
        {
            return await _userManager.GetUserAsync(principal);
        }

        public async Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            return await _userManager.HasPasswordAsync(user);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user)
        {
            return await _userManager.GetLoginsAsync(user);
        }

        public async Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
            return await _signInManager.GetExternalAuthenticationSchemesAsync();
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId)
        {
            return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, userId);
        }

        public async Task<IdentityResult> SetEmailAsync(ApplicationUser user, string email)
        {
            return await _userManager.SetEmailAsync(user, email);
        }

        public async Task<IdentityResult> SetPhoneNumberAsync(ApplicationUser user, string phoneNumber)
        {
            return await _userManager.SetPhoneNumberAsync(user, phoneNumber);
        }

        public async Task<IdentityResult> AddPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager.AddPasswordAsync(user, password);
        }

        public async Task<IdentityResult> RemoveLoginAsync(ApplicationUser user, string loginProvider, string providerKey)
        {
            return await _userManager.RemoveLoginAsync(user, loginProvider, providerKey);
        }

        public async Task<string> GetAuthenticatorKeyAsync(ApplicationUser user)
        {
            return await _userManager.GetAuthenticatorKeyAsync(user);
        }

        public async Task<int> CountRecoveryCodesAsync(ApplicationUser user)
        {
            return await _userManager.CountRecoveryCodesAsync(user);
        }

        public async Task<IdentityResult> SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled = false)
        {
            return await _userManager.SetTwoFactorEnabledAsync(user, enabled);
        }

        public async Task<bool> VerifyTwoFactorTokenAsync(ApplicationUser user, string code)
        {
            return await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, code);
        }

        public async Task<IEnumerable<string>> GenerateNewTwoFactorRecoveryCodesAsync(ApplicationUser user)
        {
            return await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
        }

        public async Task<IdentityResult> ResetAuthenticatorKeyAsync(ApplicationUser user)
        {
            return await _userManager.ResetAuthenticatorKeyAsync(user);
        }
    }
}