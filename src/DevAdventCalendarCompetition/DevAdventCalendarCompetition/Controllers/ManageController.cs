using System;
using System.Globalization;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Extensions;
using DevAdventCalendarCompetition.Models.ManageViewModels;
using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DevAdventCalendarCompetition.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : Controller
    {
#pragma warning disable IDE0051 // Remove unused private members
        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
#pragma warning restore IDE0051 // Remove unused private members

        private const string RecoveryCodesKey = nameof(RecoveryCodesKey);

        private readonly IManageService manageService;
        private readonly IAccountService accountService;
        private readonly ILogger logger;

#pragma warning disable IDE0052 // Remove unread private members
        private readonly UrlEncoder urlEncoder;
#pragma warning restore IDE0052 // Remove unread private members

        public ManageController(IManageService manageService, IAccountService accountService, ILogger<ManageController> logger, UrlEncoder urlEncoder)
        {
            this.manageService = manageService;
            this.accountService = accountService;
            this.logger = logger;
            this.urlEncoder = urlEncoder;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await this.manageService.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new ArgumentException($"Nie można załadować użytkownika z identyfikatorem '{this.accountService.GetUserId(this.User)}'.");
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = this.StatusMessage
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.manageService.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new ArgumentException($"Nie można załadować użytkownika z identyfikatorem '{this.accountService.GetUserId(this.User)}'.");
            }

            var email = user.Email;
#pragma warning disable CA1062 // Validate arguments of public methods
            if (model.Email != email)
#pragma warning restore CA1062 // Validate arguments of public methods
            {
                var setEmailResult = await this.manageService.SetEmailAsync(user, model.Email).ConfigureAwait(false);
                if (!setEmailResult.Succeeded)
                {
                    throw new ArgumentException($"Wystąpił nieoczekiwany błąd podczas konfigurowania wiadomości e-mail dla użytkownika z identyfikatorem '{user.Id}'.");
                }
            }

            var phoneNumber = user.PhoneNumber;
            if (model.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await this.manageService.SetPhoneNumberAsync(user, model.PhoneNumber).ConfigureAwait(false);
                if (!setPhoneResult.Succeeded)
                {
                    throw new ArgumentException($"Wystąpił nieoczekiwany błąd podczas ustawiania numeru telefonu dla użytkownika z identyfikatorem '{user.Id}'.");
                }
            }

            this.StatusMessage = "Profil został zaktualizowany";
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendVerificationEmail(IndexViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.manageService.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new ArgumentException($"Nie można załadować użytkownika z identyfikatorem '{this.accountService.GetUserId(this.User)}'.");
            }

            var code = await this.accountService.GenerateEmailConfirmationTokenAsync(this.User).ConfigureAwait(false);
            var callbackUrl = this.Url.EmailConfirmationLink(user.Id, code, this.Request.Scheme);
            var email = user.Email;
            await this.accountService.SendEmailConfirmationAsync(email, callbackUrl).ConfigureAwait(false);

            this.StatusMessage = "E-mail weryfikacyjny został wysłany. Sprawdź swoją skrzynkę odbiorczą";
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await this.manageService.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new ArgumentException($"Nie można załadować użytkownika z identyfikatorem '{this.accountService.GetUserId(this.User)}'.");
            }

            var hasPassword = await this.manageService.HasPasswordAsync(user).ConfigureAwait(false);
            if (!hasPassword)
            {
                return this.RedirectToAction(nameof(this.SetPassword));
            }

            var model = new ChangePasswordViewModel { StatusMessage = this.StatusMessage };
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.manageService.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new ArgumentException($"Nie można załadować użytkownika z identyfikatorem '{this.accountService.GetUserId(this.User)}'.");
            }

#pragma warning disable CA1062 // Validate arguments of public methods
            var changePasswordResult = await this.manageService.ChangePasswordAsync(user, model.OldPassword, model.NewPassword).ConfigureAwait(false);
#pragma warning restore CA1062 // Validate arguments of public methods
            if (!changePasswordResult.Succeeded)
            {
                this.AddErrors(changePasswordResult);
                return this.View(model);
            }

            await this.accountService.SignInAsync(user).ConfigureAwait(false);
#pragma warning disable CA1303 // Do not pass literals as localized parameters
            this.logger.LogInformation("User changed their password successfully.");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            this.StatusMessage = "Twoje hasło zostało zmienione";

            return this.RedirectToAction(nameof(this.ChangePassword));
        }

        [HttpGet]
        public async Task<IActionResult> SetPassword()
        {
            var user = await this.manageService.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new ArgumentException($"Nie można załadować użytkownika z identyfikatorem '{this.accountService.GetUserId(this.User)}'.");
            }

            var hasPassword = await this.manageService.HasPasswordAsync(user).ConfigureAwait(false);

            if (hasPassword)
            {
                return this.RedirectToAction(nameof(this.ChangePassword));
            }

            var model = new SetPasswordViewModel { StatusMessage = this.StatusMessage };
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.manageService.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new ArgumentException($"Nie można załadować użytkownika z identyfikatorem '{this.accountService.GetUserId(this.User)}'.");
            }

#pragma warning disable CA1062 // Validate arguments of public methods
            var addPasswordResult = await this.manageService.AddPasswordAsync(user, model.NewPassword).ConfigureAwait(false);
#pragma warning restore CA1062 // Validate arguments of public methods
            if (!addPasswordResult.Succeeded)
            {
                this.AddErrors(addPasswordResult);
                return this.View(model);
            }

            await this.accountService.SignInAsync(user).ConfigureAwait(false);
            this.StatusMessage = "Your password has been set.";

            return this.RedirectToAction(nameof(this.SetPassword));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        // TODO: move to service
#pragma warning disable IDE0051 // Remove unused private members
        private string FormatKey(string unformattedKey)
#pragma warning restore IDE0051 // Remove unused private members
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }

            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToUpper(CultureInfo.InvariantCulture);
        }
  }
}