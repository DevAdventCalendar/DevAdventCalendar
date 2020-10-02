using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using DevAdventCalendarCompetition.Controllers;
using DevAdventCalendarCompetition.Models.Account;
using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Moq;
using Xunit;

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
                HttpContext = new DefaultHttpContext() { User = null }
            };

            // Act
            var result = controller.Login(returnUrl);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        private static ClaimsPrincipal GetUser() =>
            new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                        new Claim(ClaimTypes.NameIdentifier, "UserId"),
                        new Claim(ClaimTypes.Name, "Name"),
                        new Claim(ClaimTypes.Role, "Role"),
            }));
    }
}
