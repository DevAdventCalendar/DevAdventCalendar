using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace DevAdventCalendarCompetition.Tests.UnitTests
{
    public class UserPasswordHashGenerationTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private readonly string _password = "P@ssw0rd";

        private readonly ApplicationUser _user = new ApplicationUser
        {
            AccessFailedCount = 0,
            ConcurrencyStamp = "7f38fd91-271a-40e5-8182-89e6c778bf94",
            Email = "devadventcalendar@gmail.com",
            EmailConfirmed = true,
            LockoutEnabled = false,
            NormalizedEmail = "devadventcalendar@gmail.com",
            NormalizedUserName = "devadventcalendar@gmail.com",
            SecurityStamp = "a05896b3-1408-4b47-abed-ece15177428e",
            UserName = "devadventcalendar@gmail.com"
        };

        public UserPasswordHashGenerationTest(ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
        }

        // Method taken from: https://github.com/aspnet/AspNetCore/blob/master/src/Identity/test/Shared/MockHelpers.cs
        public static UserManager<TUser> TestUserManager<TUser>(IUserStore<TUser> store = null)
            where TUser : class
        {
            store = store ?? new Mock<IUserEmailStore<TUser>>().Object;

            var options = new Mock<IOptions<IdentityOptions>>();

            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;

            options.Setup(o => o.Value).Returns(idOptions);

            var userValidators = new List<IUserValidator<TUser>>();

            var validator = new Mock<IUserValidator<TUser>>();
            userValidators.Add(validator.Object);

            var pwdValidators = new List<PasswordValidator<TUser>>();

            pwdValidators.Add(new PasswordValidator<TUser>());

            var userManager = new UserManager<TUser>(store, options.Object, new PasswordHasher<TUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<TUser>>>().Object);

            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<TUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();

            return userManager;
        }

        [Fact]
        public void GeneratePasswordHash_VerifyPasswordHashIsCorrectAndDisplayItInStandardOutput()
        {
            var userManager = TestUserManager<ApplicationUser>();

            var hasher = userManager.PasswordHasher;
            string hash = hasher.HashPassword(this._user, this._password);

            var result = hasher.VerifyHashedPassword(this._user, hash, this._password);

            Assert.Equal(PasswordVerificationResult.Success, result);
            this._testOutputHelper.WriteLine($"Generated hash value: {hash}");

            userManager.Dispose();
        }
    }
}
