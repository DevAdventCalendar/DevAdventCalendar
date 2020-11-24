using System.Threading.Tasks;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Tests.IntegrationTests.TestStartup;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Xunit;
using static DevAdventCalendarCompetition.Tests.TestHelper;

namespace DevAdventCalendarCompetition.Tests.IntegrationTests
{
    public class ManageServiceTests : StartupTestBase
    {
        [Fact]
        public async Task SetEmailAsync_CannotUpdateEmailToTheExistingOne()
        {
            // Arrange
            var users = CreateTestUsers();

            var newEmail = "test1@mail.com";
            await this.AddApplicationUserAsync(users[0]).ConfigureAwait(false);
            await this.AddApplicationUserAsync(users[1]).ConfigureAwait(false);

            // Act
            var result = await this.ExecuteAsync<IManageService, IdentityResult>(manageService =>
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
            var users = CreateTestUsers();

            var newUserName = "testUser1";
            await this.AddApplicationUserAsync(users[0]).ConfigureAwait(false);
            await this.AddApplicationUserAsync(users[1]).ConfigureAwait(false);

            // Act
            var result = await this.ExecuteAsync<IManageService, IdentityResult>(manageService =>
                manageService.SetUserNameAsync(users[0], newUserName)).ConfigureAwait(false);

            // Assert
            result.Should().NotBe(IdentityResult.Success);
            result.Errors.Should().HaveCount(1);
            result.Errors.Should().Contain(x => x.Code == "DuplicateUserName");
        }

        [Fact]
        public async Task UpdateUserAsync_CannotUpdateUserWithOtherUsersName()
        {
            // Arrange
            var users = CreateTestUsers();

            var newUserName = "testUser1";
            await this.AddApplicationUserAsync(users[0]).ConfigureAwait(false);
            await this.AddApplicationUserAsync(users[1]).ConfigureAwait(false);
            users[0].UserName = newUserName;

            // Act
            var result = await this.ExecuteAsync<IManageService, IdentityResult>(manageService =>
                manageService.UpdateUserAsync(users[0])).ConfigureAwait(false);

            // Assert
            result.Should().NotBe(IdentityResult.Success);
            result.Errors.Should().HaveCount(1);
            result.Errors.Should().Contain(x => x.Code == "DuplicateUserName");
        }

        [Fact]
        public async Task UpdateUserAsync_CannotUpdateUserWithOtherUsersEmail()
        {
            // Arrange
            var users = CreateTestUsers();

            var newEmail = "test1@mail.com";
            await this.AddApplicationUserAsync(users[0]).ConfigureAwait(false);
            await this.AddApplicationUserAsync(users[1]).ConfigureAwait(false);
            users[0].Email = newEmail;

            // Act
            var result = await this.ExecuteAsync<IManageService, IdentityResult>(manageService =>
                manageService.UpdateUserAsync(users[0])).ConfigureAwait(false);

            // Assert
            result.Should().NotBe(IdentityResult.Success);
            result.Errors.Should().HaveCount(1);
            result.Errors.Should().Contain(x => x.Code == "DuplicateEmail");
        }

        [Fact]
        public async Task UpdateUserAsync_CannotUpdateUserWithOtherUsersNameAndEmail()
        {
            // Arrange
            var users = CreateTestUsers();

            var newUserName = "testUser1";
            var newEmail = "test1@mail.com";

            await this.AddApplicationUserAsync(users[0]).ConfigureAwait(false);
            await this.AddApplicationUserAsync(users[1]).ConfigureAwait(false);

            users[0].UserName = newUserName;
            users[0].Email = newEmail;

            // Act
            var result = await this.ExecuteAsync<IManageService, IdentityResult>(manageService =>
                manageService.UpdateUserAsync(users[0])).ConfigureAwait(false);

            // Assert
            result.Should().NotBe(IdentityResult.Success);
            result.Errors.Should().HaveCount(2);
            result.Errors.Should().Contain(x => x.Code == "DuplicateUserName");
            result.Errors.Should().Contain(x => x.Code == "DuplicateEmail");
        }
    }
}
