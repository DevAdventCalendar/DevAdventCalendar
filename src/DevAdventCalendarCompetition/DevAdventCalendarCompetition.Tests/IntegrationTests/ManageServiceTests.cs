using System.Threading.Tasks;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Tests.IntegrationTests.TestStartup;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Xunit;
using static DevAdventCalendarCompetition.Tests.IntegrationTests.TestStartup.StartupTestBase;

namespace DevAdventCalendarCompetition.Tests.IntegrationTests
{
    [Collection("AccountTestCollection")]
    public class ManageServiceTests : IntegrationStartupTestBase
    {
        [Fact]
        public async Task SetEmailAsync_CannotUpdateEmailToTheExistingOne()
        {
            // Arrange
            var users = new[]
            {
                new ApplicationUser
                {
                    UserName = "testUser",
                    Email = "test@mail.com"
                },
                new ApplicationUser
                {
                    UserName = "testUser1",
                    Email = "test1@mail.com"
                },
            };

            var newEmail = "test1@mail.com";
            await AddApplicationUserAsync(users[0]).ConfigureAwait(false);
            await AddApplicationUserAsync(users[1]).ConfigureAwait(false);

            // Act
            var result = await ExecuteAsync<IManageService, IdentityResult>(manageService =>
                manageService.SetEmailAsync(users[0], newEmail)).ConfigureAwait(false);

            // Assert
            result.Should().NotBe(IdentityResult.Success);
            result.Errors.Should().HaveCount(1);
            result.Errors.Should().Contain(x => x.Code == "DuplicateEmail");
        }

        [Fact]
        public async Task SetUserNameAsync_CannotUpdateUserNameToTheExistingOne()
        {
            // Arrange
            var users = new[]
            {
                new ApplicationUser
                {
                    UserName = "testUser",
                    Email = "test@mail.com"
                },
                new ApplicationUser
                {
                    UserName = "testUser1",
                    Email = "test1@mail.com"
                },
            };

            var newUserName = "testUser1";
            await AddApplicationUserAsync(users[0]).ConfigureAwait(false);
            await AddApplicationUserAsync(users[1]).ConfigureAwait(false);

            // Act
            var result = await ExecuteAsync<IManageService, IdentityResult>(manageService =>
                manageService.SetUserNameAsync(users[0], newUserName)).ConfigureAwait(false);

            // Assert
            result.Should().NotBe(IdentityResult.Success);
            result.Errors.Should().HaveCount(1);
            result.Errors.Should().Contain(x => x.Code == "DuplicateUserName");
        }
    }
}
