using System;
using System.Collections.Generic;
using System.Linq;
using DevAdventCalendarCompetition.Controllers;
using DevAdventCalendarCompetition.Models.Account;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Resources;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Tests.UnitTests.Controllers.IdentityHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DevAdventCalendarCompetition.Tests.UnitTests.Controllers
{
    public class AccountControllerTests
    {
        private IUserValidator<ApplicationUser> _userValidator;
        private IAccountService _accountService;
        private ILogger<AccountController> _logger;

        public AccountControllerTests()
        {
            this._logger = new Mock<ILogger<AccountController>>().Object;
        }

        [Fact]
        public void Account_CannotAddUserWithExistingUsername_ThrowsException()
        {
            var users = GetUsers().AsQueryable();

            this._userValidator = new UserValidator<ApplicationUser>();
            using var userManager = new FakeUserManager(this._userValidator, users);
            this._accountService = new AccountService(null, userManager, null);

            var registerViewModel = new RegisterViewModel
            {
                UserName = "user1",
                Email = "user1@gmail.com",
                Password = "userP@ssword1",
                ConfirmPassword = "userP@ssword1",
            };
            using var controller = this.CreateAccountController();

            Func<IActionResult> result = () => controller.Register(registerViewModel).Result;

            var exception = Assert.Throws<InvalidOperationException>(result);
            Assert.Equal(exception.Message, ExceptionsMessages.UserWithNameExists);
        }

        private static List<ApplicationUser> GetUsers()
        {
            return new List<ApplicationUser>()
            {
                new ApplicationUser
                {
                    Email = "user1@gmail.com",
                    UserName = "user1"
                }
            };
        }

        private AccountController CreateAccountController()
        {
            return new AccountController(this._accountService, this._logger);
        }
    }
}
