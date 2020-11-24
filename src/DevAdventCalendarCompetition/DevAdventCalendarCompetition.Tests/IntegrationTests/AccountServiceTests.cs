using System.Threading.Tasks;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Tests.IntegrationTests.TestStartup;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace DevAdventCalendarCompetition.Tests.IntegrationTests
{
    public class AccountServiceTests : StartupTestBase
    {
        [Fact]
        public async Task CreateAsync_CannotAddUserWithExistingUsernameAndEmail()
        {
            // Arrange
            var email = "test@mail.com";
            var userName = "testUser";

            await this.AddApplicationUserAsync(new ApplicationUser
            {
                Email = email,
                UserName = userName
            }).ConfigureAwait(false);

            // Act
            var result = await this.ExecuteAsync<IAccountService, IdentityResult>(accountService
                => accountService.CreateAsync(
                    new ApplicationUser
                    {
                        Email = email,
                        UserName = userName,
                    }, "P@ssw0rd")).ConfigureAwait(false);

            // Assert
            result.Should().NotBe(IdentityResult.Success);
            result.Errors.Should().HaveCount(2);
            result.Errors.Should().Contain(x => x.Code == "DuplicateEmail");
            result.Errors.Should().Contain(x => x.Code == "DuplicateUserName");
        }

        [Fact]
        public async Task CreateAsync_CannotAddUserWithExistingUsername()
        {
            var userName = "testUser";

            await this.AddApplicationUserAsync(new ApplicationUser
            {
                Email = "test@mail.com",
                UserName = userName,
            }).ConfigureAwait(false);

            var result = await this.ExecuteAsync<IAccountService, IdentityResult>(accountService
                => accountService.CreateAsync(
                    new ApplicationUser
                    {
                        Email = "test2@mail.com",
                        UserName = userName,
                    }, "P@ssw0rd")).ConfigureAwait(false);

            result.Should().NotBe(IdentityResult.Success);
            result.Errors.Should().HaveCount(1);
            result.Errors.Should().Contain(x => x.Code == "DuplicateUserName");
        }

        [Fact]
        public async Task CreateAsync_CannotAddUserWithExistingEmail()
        {
            var email = "test@mail.com";

            await this.AddApplicationUserAsync(new ApplicationUser
            {
                Email = email,
                UserName = "testUser",
            }).ConfigureAwait(false);

            var result = await this.ExecuteAsync<IAccountService, IdentityResult>(accountService
                => accountService.CreateAsync(
                    new ApplicationUser
                    {
                        Email = email,
                        UserName = "testUser2",
                    }, "P@ssw0rd")).ConfigureAwait(false);

            result.Should().NotBe(IdentityResult.Success);
            result.Errors.Should().HaveCount(1);
            result.Errors.Should().Contain(x => x.Code == "DuplicateEmail");
        }
    }
}
