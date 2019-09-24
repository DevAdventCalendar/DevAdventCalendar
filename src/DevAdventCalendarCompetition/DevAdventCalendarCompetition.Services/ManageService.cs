using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace DevAdventCalendarCompetition.Services
{
    public class ManageService : IManageService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public ManageService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._emailSender = emailSender;
        }

        public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal)
        {
            return await this._userManager.GetUserAsync(principal).ConfigureAwait(false);
        }

        public async Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            return await this._userManager.HasPasswordAsync(user).ConfigureAwait(false);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword)
        {
            return await this._userManager.ChangePasswordAsync(user, oldPassword, newPassword).ConfigureAwait(false);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user)
        {
            return await this._userManager.GetLoginsAsync(user).ConfigureAwait(false);
        }

        public async Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
            return await this._signInManager.GetExternalAuthenticationSchemesAsync().ConfigureAwait(false);
        }

        public async Task<IdentityResult> SetEmailAsync(ApplicationUser user, string email)
        {
            return await this._userManager.SetEmailAsync(user, email).ConfigureAwait(false);
        }

        public async Task<IdentityResult> SetPhoneNumberAsync(ApplicationUser user, string phoneNumber)
        {
            return await this._userManager.SetPhoneNumberAsync(user, phoneNumber).ConfigureAwait(false);
        }

        public async Task<IdentityResult> AddPasswordAsync(ApplicationUser user, string password)
        {
            return await this._userManager.AddPasswordAsync(user, password).ConfigureAwait(false);
        }

        public async Task<IdentityResult> RemoveLoginAsync(ApplicationUser user, string loginProvider, string providerKey)
        {
            return await this._userManager.RemoveLoginAsync(user, loginProvider, providerKey).ConfigureAwait(false);
        }
    }
}