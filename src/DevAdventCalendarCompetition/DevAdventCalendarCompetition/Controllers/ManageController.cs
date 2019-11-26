using System;
using System.Globalization;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Extensions;
using DevAdventCalendarCompetition.Models.ManageViewModels;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevExeptionsMessages;
using DevLoggingMessages;
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
        private const string RecoveryCodesKey = nameof(RecoveryCodesKey);

        private readonly IManageService manageService;
        private readonly IAccountService accountService;
        private readonly ILogger logger;
        private readonly INotificationService _emailNotificationService;

        public ManageController(IManageService manageService, IAccountService accountService, ILogger<ManageController> logger, INotificationService emailNotificationService)
        {
            this.manageService = manageService ?? throw new ArgumentNullException(nameof(manageService));
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._emailNotificationService = emailNotificationService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await this.manageService.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, ExceptionsMessages.UserWithIdNotFound, this.accountService.GetUserId(this.User)));
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                EmailNotificationsEnabled = user.EmailNotificationsEnabled,
                PushNotificationsEnabled = user.PushNotificationsEnabled,
                StatusMessage = this.StatusMessage
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.manageService.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, ExceptionsMessages.UserWithIdNotFound, this.accountService.GetUserId(this.User)));
            }

            var email = user.Email;
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var shouldSendVerificationEmail = false;
            if (model.Email != email)
            {
                var setEmailResult = await this.manageService.SetEmailAsync(user, model.Email).ConfigureAwait(false);
                if (!setEmailResult.Succeeded)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, ExceptionsMessages.ErrorDuringEmailConfiguration, this.accountService.GetUserId(this.User)));
                }

                shouldSendVerificationEmail = true;
                model.EmailNotificationsEnabled = false;
            }

            var phoneNumber = user.PhoneNumber;
            if (model.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await this.manageService.SetPhoneNumberAsync(user, model.PhoneNumber).ConfigureAwait(false);
                if (!setPhoneResult.Succeeded)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, ExceptionsMessages.ErrorDuringPhoneNumberConfiguration, this.accountService.GetUserId(this.User)));
                }
            }

            if (model.EmailNotificationsEnabled != user.EmailNotificationsEnabled)
            {
                user.EmailNotificationsEnabled = model.EmailNotificationsEnabled;
                var updateEmailNotificationPreferencesSucceeded =
                    await this._emailNotificationService
                        .SetSubscriptionPreferenceAsync(user.Email, user.EmailNotificationsEnabled && user.EmailConfirmed)
                        .ConfigureAwait(false)
                    && (await this.manageService.UpdateUserAsync(user).ConfigureAwait(false)).Succeeded;

                if (updateEmailNotificationPreferencesSucceeded == false)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, ExceptionsMessages.ErrorDuringEmailNotificationsPreferenceChange, this.accountService.GetUserId(this.User)));
                }
            }

            if (model.PushNotificationsEnabled != user.PushNotificationsEnabled)
            {
                user.PushNotificationsEnabled = model.PushNotificationsEnabled;
                var updatePushNotificationPreferencesSucceeded = (await this.manageService.UpdateUserAsync(user).ConfigureAwait(false)).Succeeded;

                if (updatePushNotificationPreferencesSucceeded == false)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, ExceptionsMessages.ErrorDuringPushNotificationsPreferenceChange, this.accountService.GetUserId(this.User)));
                }
            }

            if (shouldSendVerificationEmail)
            {
                return await this.SendVerificationEmail(model).ConfigureAwait(false);
            }

            this.StatusMessage = "Profil został zaktualizowany";
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendVerificationEmail(IndexViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.manageService.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, ExceptionsMessages.UserWithIdNotFound, this.accountService.GetUserId(this.User)));
            }

            var code = await this.accountService.GenerateEmailConfirmationTokenAsync(this.User).ConfigureAwait(false);
            var callbackUrl = this.Url.EmailConfirmationLink(user.Id, code, this.Request.Scheme);
            var email = user.Email;
            await this.accountService.SendEmailConfirmationAsync(email, new Uri(callbackUrl)).ConfigureAwait(false);

            this.StatusMessage = "E-mail weryfikacyjny został wysłany. Sprawdź swoją skrzynkę odbiorczą";
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await this.manageService.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, ExceptionsMessages.UserWithIdNotFound, this.accountService.GetUserId(this.User)));
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
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.manageService.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, ExceptionsMessages.UserWithIdNotFound, this.accountService.GetUserId(this.User)));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var changePasswordResult = await this.manageService.ChangePasswordAsync(user, model.OldPassword, model.NewPassword).ConfigureAwait(false);

            if (!changePasswordResult.Succeeded)
            {
                this.AddErrors(changePasswordResult);
                return this.View(model);
            }

            await this.accountService.SignInAsync(user).ConfigureAwait(false);
            this.logger.LogInformation(LoggingMessages.PasswordIsChangedSuccessfully);
            this.StatusMessage = "Twoje hasło zostało zmienione";

            return this.RedirectToAction(nameof(this.ChangePassword));
        }

        [HttpGet]
        public async Task<IActionResult> SetPassword()
        {
            var user = await this.manageService.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, ExceptionsMessages.UserWithIdNotFound, this.accountService.GetUserId(this.User)));
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
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.manageService.GetUserAsync(this.User).ConfigureAwait(false);
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, ExceptionsMessages.UserWithIdNotFound, this.accountService.GetUserId(this.User)));
            }

            var addPasswordResult = await this.manageService.AddPasswordAsync(user, model.NewPassword).ConfigureAwait(false);
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
  }
}