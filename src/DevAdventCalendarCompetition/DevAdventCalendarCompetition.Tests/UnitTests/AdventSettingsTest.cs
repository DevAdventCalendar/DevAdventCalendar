using System;
using System.Collections.Generic;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Options;
using Xunit;

namespace DevAdventCalendarCompetition.Tests.UnitTests
{
    public class AdventSettingsTest
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
            using var context = new DateTimeProviderContext(actual);
            var sut = new AdventSettings()
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var result = sut.IsAdvent();

            Assert.Equal(expected, result);
        }
    }
}
