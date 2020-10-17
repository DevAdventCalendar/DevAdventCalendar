using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Castle.Core.Logging;
using DevAdventCalendarCompetition.Controllers;
using DevAdventCalendarCompetition.Models.Account;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Moq;
using Xunit;
using static DevAdventCalendarCompetition.Resources.ViewsMessages;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace DevAdventCalendarCompetition.Tests.UnitTests.Controllers
{
    public class AccountControllerTests
    {
        private readonly Mock<IAccountService> _accountServiceMock;
        private readonly Mock<ILogger<AccountController>> _loggerMock;

        public AccountControllerTests()
        {
            this._accountServiceMock = new Mock<IAccountService>();
            this._loggerMock = new Mock<ILogger<AccountController>>();
        }

        [Fact]
        public void Login_ReturnsLoginViewModel()
        {
            // Arrange
            Uri returnUrl = null;
            using var controller = new AccountController(this._accountServiceMock.Object, this._loggerMock.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { RequestServices = GetRequestService() }
            };

            // Act
            var result = controller.Login(returnUrl);

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void Login_LoginViewModelIsNull_ThrowsException()
        {
            // Arrange
            Uri returnUrl = null;
            LoginViewModel model = null;
            using var controller = new AccountController(this._accountServiceMock.Object, this._loggerMock.Object);

            // Act
            Func<Task<IActionResult>> act = () => controller.Login(model, returnUrl);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact]
        public void Login_UserIsNull_ReturnsViewWithError()
        {
            // Arrange
            Uri returnUrl = null;
            LoginViewModel model = GetLoginViewModel();
            this._accountServiceMock.Setup(x => x.FindByEmailAsync(model.Email)).ReturnsAsync((ApplicationUser)null);
            using var controller = new AccountController(this._accountServiceMock.Object, this._loggerMock.Object);

            // Act
            var result = controller.Login(model, returnUrl);

            // Assert
            var allErrors = controller.ModelState.Values.SelectMany(v => v.Errors);
            Assert.Single(allErrors);
            Assert.Contains(allErrors, x => x.ErrorMessage == @EmailNotFound);
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void Login_EmailNotConfirmed_ReturnsViewWithError()
        {
            // Arrange
            Uri returnUrl = null;
            LoginViewModel model = GetLoginViewModel();
            this._accountServiceMock.Setup(x => x.FindByEmailAsync(model.Email))
                                    .ReturnsAsync(new ApplicationUser() { EmailConfirmed = false });
            using var controller = new AccountController(this._accountServiceMock.Object, this._loggerMock.Object);

            // Act
            var result = controller.Login(model, returnUrl);

            // Assert
            var allErrors = controller.ModelState.Values.SelectMany(v => v.Errors);
            Assert.Single(allErrors);
            Assert.Contains(allErrors, x => x.ErrorMessage == @EmailMustBeConfirmed);
            var viewResult = Assert.IsType<ViewResult>(result.Result);
            Assert.IsType<LoginViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public void Login_ResultIsSucceeded_RedirectToLocal()
        {
            // Arrange
            Uri returnUrl = null;
            LoginViewModel model = GetLoginViewModel();
            this._accountServiceMock.Setup(x => x.FindByEmailAsync(model.Email))
                                    .ReturnsAsync(new ApplicationUser() { EmailConfirmed = true });
            this._accountServiceMock.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), model.Password, model.RememberMe))
                                   .ReturnsAsync(SignInResult.Success);
            using var controller = new AccountController(this._accountServiceMock.Object, this._loggerMock.Object);
            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Default);
            mockUrlHelper.Setup(x => x.Action(It.IsAny<UrlActionContext>())).Returns("callbackUrl").Verifiable();
            controller.Url = mockUrlHelper.Object;

            // Act
            var result = controller.Login(model, returnUrl);

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Index", viewResult.ActionName);
        }

        [Fact]
        public void Login_ResultIsLockedOut_RedirectToLocal()
        {
            // Arrange
            Uri returnUrl = null;
            LoginViewModel model = GetLoginViewModel();
            this._accountServiceMock.Setup(x => x.FindByEmailAsync(model.Email))
                                    .ReturnsAsync(new ApplicationUser() { EmailConfirmed = true });
            this._accountServiceMock.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), model.Password, model.RememberMe))
                                   .ReturnsAsync(SignInResult.LockedOut);
            using var controller = new AccountController(this._accountServiceMock.Object, this._loggerMock.Object);

            // Act
            var result = controller.Login(model, returnUrl);

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Lockout", viewResult.ActionName);
        }

        [Fact]
        public void Login_FailedLogin_ReturnsViewWithError()
        {
            // Arrange
            Uri returnUrl = null;
            LoginViewModel model = GetLoginViewModel();
            this._accountServiceMock.Setup(x => x.FindByEmailAsync(model.Email))
                                    .ReturnsAsync(new ApplicationUser() { EmailConfirmed = true });
            this._accountServiceMock.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), model.Password, model.RememberMe))
                                   .ReturnsAsync(SignInResult.Failed);
            using var controller = new AccountController(this._accountServiceMock.Object, this._loggerMock.Object);

            // Act
            var result = controller.Login(model, returnUrl);

            // Assert
            var allErrors = controller.ModelState.Values.SelectMany(v => v.Errors);
            Assert.Single(allErrors);
            Assert.Contains(allErrors, x => x.ErrorMessage == @FailedLoginAttempt);
            var viewResult = Assert.IsType<ViewResult>(result.Result);
            Assert.IsType<LoginViewModel>(viewResult.ViewData.Model);
        }

        private static LoginViewModel GetLoginViewModel()
        {
            return new LoginViewModel
            {
                Email = "a@gmail.com",
                Password = "password",
                RememberMe = false
            };
        }

        private static ClaimsPrincipal GetUser() =>
            new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                        new Claim(ClaimTypes.NameIdentifier, "UserId"),
                        new Claim(ClaimTypes.Name, "Name"),
                        new Claim(ClaimTypes.Role, "Role"),
            }));

        private static IServiceProvider GetRequestService()
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock
                .Setup(_ => _.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.FromResult((object)null));

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(_ => _.GetService(typeof(IAuthenticationService)))
                .Returns(authServiceMock.Object);

            return serviceProviderMock.Object;
        }
    }
}
