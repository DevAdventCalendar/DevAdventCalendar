namespace DevAdventCalendarCompetition.Tests.UnitTests
{
    public class EmailSenderExtensionsTest
    {
        [Fact]
        public void SendEmailConfirmationForNewEmail()
        {
            // Arrange
            //mock emailSender
            //set testEmail and testLink

            // Act
            EmailSenderExtensions.SendEmailConfirmationAsync(emailSenderMock, testEmail, testLink, true);

            // Assert
            this._testRepositoryMock.Verify(mock => emailSenderMock.SendEmailAsync(testEmail, "Potwierdzenie zmiany email", It.IsAny<string>()), Times.Once());
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
