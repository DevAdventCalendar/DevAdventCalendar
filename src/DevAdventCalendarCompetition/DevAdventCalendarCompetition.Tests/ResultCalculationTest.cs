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

        private readonly List<CompetitionResult> _results = new List<CompetitionResult>
        {
            new CompetitionResult { UserId = "1", Points = 100, AnsweringTimeOffset = 112215 },
            new CompetitionResult { UserId = "2", Points = 500, AnsweringTimeOffset = 47774, },
            new CompetitionResult { UserId = "3", Points = 300, AnsweringTimeOffset = 3001121, },
            new CompetitionResult { UserId = "4", Points = 120, AnsweringTimeOffset = 132215, },
            new CompetitionResult { UserId = "5", Points = 100, AnsweringTimeOffset = 109655, },
            new CompetitionResult { UserId = "6", Points = 100, AnsweringTimeOffset = 2327750 },
        };

        private ITestResultPointsRule _correctAnswersPointsRule;
        private ITestResultPointsRule _bonusPointsRule;
        private ITestResultPlaceRule _placeRule;

        public ResultCalculationTest()
        {
            this._correctAnswersPointsRule = new CorrectAnswerPointsRule();
            this._bonusPointsRule = new BonusPointsRule();
            this._placeRule = new AnsweringTimePlaceRule();
        }

        [Fact]
        public void UserWithCorrectAnswersShouldGetPositivePointsCount()
        {
            var result = this._correctAnswersPointsRule.CalculatePoints(this._answers.Count);

            Assert.True(result > 0, "User who answered one or more tests cannot get 0 points.");
        }

        [Fact]
        public void ForGivenWrongAnswersCountProperBonusPointsShouldBeReturned()
        {
            var result = this._bonusPointsRule.CalculatePoints(this._wrongAnswers.Count);

            Assert.Equal(15, result);
        }

        [Fact]
        public void UsersWithSamePointsCountShouldGetPlaceBasedOnAscOffsetOrder()
        {
            var orderedResults = this._placeRule.GetUsersOrder(this._results);

            Assert.Equal("2", orderedResults[0].UserId);
            Assert.Equal("3", orderedResults[1].UserId);
            Assert.Equal("4", orderedResults[2].UserId);
            Assert.Equal("5", orderedResults[3].UserId);
            Assert.Equal("1", orderedResults[4].UserId);
            Assert.Equal("6", orderedResults[5].UserId);
        }
    }
}
