using System;
using System.Collections.Generic;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Options;
using Moq;
using Xunit;

namespace DevAdventCalendarCompetition.Tests.UnitTests
{
    public class AdventServiceTest
    {
        public static List<object[]> Dates()
        {
            return new List<object[]>
            {
                new object[] { new DateTime(2020, 12, 1), new DateTime(2020, 12, 24), new DateTime(2020, 11, 24), false },
                new object[] { new DateTime(2020, 12, 1), new DateTime(2020, 12, 24), new DateTime(2020, 12, 12), true },
                new object[] { new DateTime(2020, 12, 1), new DateTime(2020, 12, 24), new DateTime(2020, 12, 2), true },
                new object[] { new DateTime(2020, 12, 1), new DateTime(2020, 12, 24), new DateTime(2020, 06, 23), false }
            };
        }

        [Theory]
        [MemberData(nameof(Dates))]
        public void IsAdvent_CorrectlyChecksDates(DateTime startDate, DateTime endDate, DateTime actual, bool expected)
        {
            // Arrange
            var settings = new AdventSettings()
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var dateTimeServiceMock = new Mock<IDateTimeService>();
            dateTimeServiceMock.Setup(x => x.Now).Returns(actual);

            var sut = new AdventService(settings, dateTimeServiceMock.Object);

            // Act
            var result = sut.IsAdvent();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
