using System;
using DevAdventCalendarCompetition.Providers;
using Xunit;

namespace DevAdventCalendarCompetition.Tests
{
    public class ErrorMessagesProviderTest
    {
        [Fact]
        public void GetMessageBody_ReturnsNotFoundMessageForNotFoundStatusCode()
        {
            // Arrange
            // Act
            var result = ErrorMessagesProvider.GetMessageBody(404);

            // Assert
            Assert.Contains("Niestety, podana strona nie istnieje :(", result.Value, StringComparison.InvariantCulture);
        }

        [Fact]
        public void GetMessageBody_ReturnsDefaultMessageForOtherStatusCode()
        {
            // Arrange
            // Act
            var result = ErrorMessagesProvider.GetMessageBody(0);

            // Assert
            Assert.Contains($"Ups... Coś poszło nie tak... Nasze elfy już nad tym pracują ;-)", result.Value, StringComparison.InvariantCulture);
        }
    }
}