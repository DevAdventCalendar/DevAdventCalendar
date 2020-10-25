using System.Collections.Generic;
using DevAdventCalendarCompetition.Services;
using FluentAssertions;
using Xunit;

namespace DevAdventCalendarCompetition.Tests.UnitTests
{
    public class AnswerServiceTest
    {
        public static List<object[]> SpacesReplaceData()
        {
            return new List<object[]>
            {
                new object[] { "Test answer", "TEST ANSWER" },
                new object[] { "  Test answer  ", "TEST ANSWER" },
                new object[] { "    Test     answer    ", "TEST ANSWER" }
            };
        }

        [Theory]
        [MemberData(nameof(SpacesReplaceData))]
        public void InUserAnswer_ShouldReplaceWhitespacesToSingleSpace(string userAnswer, string resultAnswer)
        {
            var sut = new AnswerService();

            var result = sut.ParseUserAnswer(userAnswer);

            result.Should().Be(resultAnswer);
        }
    }
}
