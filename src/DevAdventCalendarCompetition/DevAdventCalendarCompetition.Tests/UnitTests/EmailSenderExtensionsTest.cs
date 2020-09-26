using System.Threading.Tasks;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Extensions;
using DevAdventCalendarCompetition.Services.Interfaces;
using Moq;
using Xunit;

namespace DevAdventCalendarCompetition.Tests.UnitTests
{
    public class EmailSenderExtensionsTest
    {
        private const string TESTEMAIL = "aa@aa.pl";
        private const string TESTLINK = "www.google.com";

        [Fact]
        public async Task SendEmailConfirmationForNewEmail()
        {
            // Arrange
            var emailSenderMock = new Mock<IEmailSender>();

            // Act
            await EmailSenderExtensions.SendEmailConfirmationAsync(emailSenderMock.Object, TESTEMAIL, TESTLINK, true).ConfigureAwait(false);

            // Assert
            emailSenderMock.Verify(
                mock => mock.SendEmailAsync(
                TESTEMAIL,
                "Potwierdzenie zmiany email",
                It.Is<string>(str => str.StartsWith("Prosimy o potwierdzenie zmiany email poprzez kliknięcie na link", System.StringComparison.InvariantCulture))),
                Times.Once());
        }

        [Fact]
        public async Task SendEmailConfirmationForNewRegistration()
        {
            // Arrange
            var emailSenderMock = new Mock<IEmailSender>();

            // Act
            await EmailSenderExtensions.SendEmailConfirmationAsync(emailSenderMock.Object, TESTEMAIL, TESTLINK, false).ConfigureAwait(false);

            // Assert
            emailSenderMock.Verify(
                mock => mock.SendEmailAsync(
                TESTEMAIL,
                "Potwierdzenie założenia konta",
                It.Is<string>(str => str.StartsWith("Prosimy o potwierdzenie założenia konta poprzez kliknięcie na link", System.StringComparison.InvariantCulture))),
                Times.Once());
        }
    }
}
