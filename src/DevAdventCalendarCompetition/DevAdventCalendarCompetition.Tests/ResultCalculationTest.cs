using System;
using System.Collections.Generic;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.TestResultService;
using DevAdventCalendarCompetition.TestResultService.Interfaces;
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

        private readonly List<TestWrongAnswer> _wrongAnswers = new List<TestWrongAnswer>
        {
            new TestWrongAnswer { Id = 1, UserId = "1", TestId = 1 },
            new TestWrongAnswer { Id = 2, UserId = "1", TestId = 1 },
            new TestWrongAnswer { Id = 3, UserId = "1", TestId = 2 }
        };

        private ITestResultPointsRule _correctAnswersPointsRule;
        private ITestResultPointsRule _wrongAnswerPointsRule;

        public ResultCalculationTest()
        {
            this._correctAnswersPointsRule = new CorrectAnswerPointsRule();
            this._wrongAnswerPointsRule = new WrongAnswerPointsRule();
        }

        [Fact]
        public void UserWithCorrectAnswersShouldGetPositivePointsNumber()
        {
            var result = this._correctAnswersPointsRule.CalculatePoints(this._answers.Count);

            Assert.True(result > 0, "User who answered one or more tests cannot get 0 points.");
        }

        [Fact]
        public void UserCannotGetLessThanZeroWrongAnswersPoints()
        {
            var result = this._wrongAnswerPointsRule.CalculatePoints(this._wrongAnswers.Count);
        }
    }
}
