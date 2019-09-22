using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IManageService
    {
        Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal);

        Task<bool> HasPasswordAsync(ApplicationUser user);

        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword);

        Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user);

        Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();

        Task<IdentityResult> SetEmailAsync(ApplicationUser user, string email);

        Task<IdentityResult> SetPhoneNumberAsync(ApplicationUser user, string phoneNumber);

        Task<IdentityResult> AddPasswordAsync(ApplicationUser user, string password);

        Task<IdentityResult> RemoveLoginAsync(ApplicationUser user, string loginProvider, string providerKey);
    }
}