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
                new object[] { "    Test     answer    ", "TEST ANSWER" },
                new object[] { " test answer test answer ", "TEST ANSWER TEST ANSWER" },
                new object[] { "TEST    answer     ", "TEST ANSWER" },
                new object[] { "        Test    answer   Test answer TEST", "TEST ANSWER TEST ANSWER TEST" },
            };
        }

        [Theory]
        [MemberData(nameof(SpacesReplaceData))]
        public void ParseTestAnswer_ShouldReplaceWhitespacesToSingleSpace(string userAnswer, string resultAnswer)
        {
            var sut = new AnswerService();

            var result = sut.ParseTestAnswer(userAnswer);

            result.Should().Be(resultAnswer);
        }
    }
}
