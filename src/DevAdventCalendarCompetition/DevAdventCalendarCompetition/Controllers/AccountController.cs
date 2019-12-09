using System;
using System.Resources;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using DevAdventCalendarCompetition.Extensions;
using DevAdventCalendarCompetition.Models.AccountViewModels;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevLoggingMessages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DevAdventCalendarCompetition.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ILogger _logger;

        public AccountController(
            IAccountService accountService,
            ILogger<AccountController> logger)
        {
            this._accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Uri returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme).ConfigureAwait(false);

            var model = new LoginViewModel();

            this.ViewData["ReturnUrl"] = returnUrl;
            return this.View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, Uri returnUrl = null)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            this.ViewData["ReturnUrl"] = returnUrl;
            if (this.ModelState.IsValid)
            {
                var user = await this._accountService.FindByEmailAsync(model.Email).ConfigureAwait(false);

                if (user == null)
                {
                    this._logger.LogWarning($"User {model.Email} not exists.");
                    this.ModelState.AddModelError(string.Empty, "Nie znaleziono takiego konta.");
                    return this.View(model);
                }

                if (!user.EmailConfirmed)
                {
                    this._logger.LogInformation(LoggingMessages.UserIsNotConfirmed);
                    this.ModelState.AddModelError(string.Empty, "Musisz najpierw potwierdzić swoje konto!");
                    return this.View(model);
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await this._accountService.PasswordSignInAsync(model.Email, model.Password, model.RememberMe).ConfigureAwait(false);

                if (result.Succeeded)
                {
                    this._logger.LogInformation(LoggingMessages.UserIsLoggedIn);
                    return this.RedirectToLocal(returnUrl?.ToString());
                }

                if (result.IsLockedOut)
                {
                    this._logger.LogWarning(LoggingMessages.UserAccountIsLockedOut);

                    return this.RedirectToAction(nameof(this.Lockout));
                }

                this.ModelState.AddModelError(string.Empty, "Niepoprawna próba logowania.");
                return this.View(model);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return this.View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(Uri returnUrl = null, string email = null)
        {
            this.ViewData["ReturnUrl"] = returnUrl;
            this.ViewData["Email"] = email;
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, Uri returnUrl = null)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            this.ViewData["ReturnUrl"] = returnUrl;
            if (this.ModelState.IsValid)
            {
                var user = this._accountService.CreateApplicationUserByEmail(model.Email);

                var result = await this._accountService.CreateAsync(user, model.Password).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    this._logger.LogInformation(LoggingMessages.NewAccount);

                    var code = await this._accountService.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
                    var callbackUrl = this.Url.EmailConfirmationLink(user.Id, code, this.Request.Scheme);
                    await this._accountService.SendEmailConfirmationAsync(model.Email, new Uri(callbackUrl)).ConfigureAwait(false);

                    return this.View("RegisterConfirmation");
                }

                this.AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await this._accountService.SignOutAsync().ConfigureAwait(false);

            this._logger.LogInformation(LoggingMessages.UserIsLoggedIn);

            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, Uri returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = this.Url.Action(nameof(this.ExternalLoginCallback), "Account", new { returnUrl });
            var properties = this._accountService.ConfigureExternalAuthenticationProperties(provider, new Uri(redirectUrl));
            return this.Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(Uri returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                this.ErrorMessage = $"Błąd od zewnętrznego dostawcy: {remoteError}";
                return this.RedirectToAction(nameof(this.Login));
            }

            var info = await this._accountService.GetExternalLoginInfoAsync().ConfigureAwait(false);
            if (info == null)
            {
                return this.RedirectToAction(nameof(this.Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await this._accountService.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey).ConfigureAwait(false);
            if (result.Succeeded)
            {
                this._logger.LogInformation(LoggingMessages.UserLoginProvider, info.LoginProvider);
                return this.RedirectToLocal(returnUrl?.ToString());
            }

            if (result.IsNotAllowed)
            {
                return this.View("RegisterConfirmation");
            }

            if (result.IsLockedOut)
            {
                return this.RedirectToAction(nameof(this.Lockout));
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                this.ViewData["ReturnUrl"] = returnUrl;
                this.ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return this.View("ExternalLogin", new ExternalLoginViewModel { Email = email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, Uri returnUrl = null)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (this.ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await this._accountService.GetExternalLoginInfoAsync().ConfigureAwait(false);
                if (info == null)
                {
                    throw new ArgumentException(LoggingMessages.LoadingDataError);
                }

                var user = this._accountService.CreateApplicationUserByEmail(model.Email);

                var result = await this._accountService.CreateAsync(user, null).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    result = await this._accountService.AddLoginAsync(user, info).ConfigureAwait(false);
                    if (result.Succeeded)
                    {
                        this._logger.LogInformation(LoggingMessages.CreatedUserNameProvider, info.LoginProvider);

                        var code = await this._accountService.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
                        var callbackUrl = this.Url.EmailConfirmationLink(user.Id, code, this.Request.Scheme);
                        await this._accountService.SendEmailConfirmationAsync(model.Email, new Uri(callbackUrl)).ConfigureAwait(false);

                        return this.View("RegisterConfirmation");
                    }
                }

                this.AddErrors(result);
            }

            this.ViewData["ReturnUrl"] = returnUrl;
            return this.View(nameof(this.ExternalLogin), model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return this.RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var user = await this._accountService.FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                throw new ArgumentException($"Nie można załadować użytkownika z identyfikatorem '{userId}'.");
            }

            var result = await this._accountService.ConfirmEmailAsync(user, code).ConfigureAwait(false);
            return this.View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (this.ModelState.IsValid)
            {
                var user = await this._accountService.FindByEmailAsync(model.Email).ConfigureAwait(false);

                if (user == null || !(await this._accountService.IsEmailConfirmedAsync(user).ConfigureAwait(false)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                     return this.Redirect("EmailIsNotExist.cshtml");
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await this._accountService.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
                var callbackUrl = this.Url.ResetPasswordCallbackLink(user.Id, code, user.Email, this.Request.Scheme);
                await this._accountService.SendEmailAsync(
                    model.Email,
                    "Reset hasła",
                    $"Swoje hasło zresetujesz klikając na link: <a href='{callbackUrl}'>LINK</a>").ConfigureAwait(false);
                return this.RedirectToAction(nameof(this.ForgotPasswordConfirmation));
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return this.View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string email, string code = null)
        {
            if (code == null)
            {
                throw new ArgumentException(LoggingMessages.ResetPasswordCodeError);
            }

            var model = new ResetPasswordViewModel { Code = code, Email = email };
            return this.View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this._accountService.FindByEmailAsync(model.Email).ConfigureAwait(false);

            if (user == null)
            {
                // Don't reveal that the user does not exist
                return this.RedirectToAction(nameof(this.ResetPasswordConfirmation));
            }

            var result = await this._accountService.ResetPasswordAsync(user, model.Code, model.Password).ConfigureAwait(false);
            if (result.Succeeded)
            {
                return this.RedirectToAction(nameof(this.ResetPasswordConfirmation));
            }

            this.AddErrors(result);

            model = new ResetPasswordViewModel
            {
                Email = model.Email
            };

            return this.View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return this.View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            else
            {
                return this.RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}