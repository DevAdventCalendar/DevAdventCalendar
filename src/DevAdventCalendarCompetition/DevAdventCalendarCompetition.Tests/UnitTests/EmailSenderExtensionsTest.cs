using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Extensions;
using DevAdventCalendarCompetition.Services.Interfaces;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DevAdventCalendarCompetition.Tests.UnitTests
{
    public class EmailSenderExtensionsTest
    {
        [Fact]
        public async Task SendEmailConfirmationForNewEmail()
        {
            var emailSenderMock = new Mock<IEmailSender>();

            var testEmail = "Potwierdzenie zmiany email";
            var testLink = "a";

            // Act
            await EmailSenderExtensions.SendEmailConfirmationAsync(emailSenderMock, testEmail, testLink, true).ConfigureAwait;

            // Assert
            emailSenderMock.Verify(mock => mock.SendEmailAsync(testEmail, "Potwierdzenie zmiany email", It.IsAny<string>()), Times.Once());
        }
        
        [Fact]
        public void SendEmailConfirmationForNewAccount()
        {
            // Arrange
            //mock emailSender
            //set testEmail and testLink

            // Act
            EmailSenderExtensions.SendEmailConfirmationAsync(emailSenderMock, testEmail, testLink, false);

            // Assert
            this._testRepositoryMock.Verify(mock => emailSenderMock.SendEmailAsync(testEmail, "Potwierdzenie założenia konta", It.IsAny<string>()), Times.Once());
        }
    }
}
