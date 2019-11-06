using System;
using System.Collections.Generic;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.TestResultService;
using Xunit;

namespace DevAdventCalendarCompetition.Tests
{
    public class ResultCalculationTest
    {
        private readonly List<TestAnswer> _answers = new List<TestAnswer>
        {
            new TestAnswer { Id = 1, UserId = "1", TestId = 1, AnsweringTime = DateTime.Now, AnsweringTimeOffset = default },
            new TestAnswer { Id = 2, UserId = "1", TestId = 1, AnsweringTime = DateTime.Today.AddHours(-2), AnsweringTimeOffset = default },
            new TestAnswer { Id = 3, UserId = "1", TestId = 2, AnsweringTime = DateTime.Today.AddHours(-1), AnsweringTimeOffset = default }
        };

        private ITestResultPointsRule _correctAnswersPointRule;

        public ResultCalculationTest()
        {
            this._correctAnswersPointRule = new CorrectAnswerPointsRule();
        }

        [Fact]
        public void GetUserCorrectAnswers_ReturnPositivePointsCount()
        {
            var result = this._correctAnswersPointRule.CalculatePoints(this._answers);

            Assert.True(result > 0, "User who answered one or more tests cannot get 0 points.");
        }
    }
}
