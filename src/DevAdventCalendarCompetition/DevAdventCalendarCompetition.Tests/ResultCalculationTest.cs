using System;
using System.Collections.Generic;
using System.Text;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.TestResultService;
using Moq;
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

        private Mock<ICorrectAnswerPointsRule> _correctAnswersPointRuleMock;

        public ResultCalculationTest()
        {
            this._correctAnswersPointRuleMock = new Mock<ICorrectAnswerPointsRule>();
        }

        [Fact]
        public void GetUserCorrectAnswers_ReturnPointsCount()
        {
            this._correctAnswersPointRuleMock.Setup(mock => mock.CalculatePoints(this._answers));
        }
    }
}
