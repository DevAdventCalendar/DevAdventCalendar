using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AutoMapper;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace DevAdventCalendarCompetition.Tests.UnitTests
{
    public class StatisticsServiceTests
    {
        private readonly StatisticsService _controller;
        private readonly Mock<ITestStatisticsService> _testStatisticsMock;

        // might me useless
        private readonly Mock<UserTestCorrectAnswer> _testUserTestCorrectAnswerMock;

        public StatisticsServiceTests()
        {
            var iDontBelongHere = new UserTestCorrectAnswer { Id = 1, UserId = "1", TestId = 1, AnsweringTime = DateTime.Now, AnsweringTimeOffset = default };

            this._testStatisticsMock = new Mock<ITestStatisticsService>();

            // might be useless
            this._testUserTestCorrectAnswerMock = new Mock<UserTestCorrectAnswer>();

            this._controller = new StatisticsService(this._testStatisticsMock.Object);
        }

        [Fact]
        public void TemporaryTest()
        {
            int a = this._controller.TmpStatisticsImpementation("1", 2);

            Assert.True(a == 3, "Temoporary test - wrong!");
        }

        [Fact]
        public void TemporaryTest2()
        {
            DateTime currentTest = DateTime.Now;
            this._testStatisticsMock.Setup(a => a.GetUserTestCorrectAnswerDate("1", 1)).Returns(currentTest);

            var result = this._controller.GetCorrectAnswerDate("1", 1);

            Assert.True(result == currentTest, "Temoporary test for datetime - wrong!");
        }

        [Fact]
        public void TemporaryTest3()
        {
            this._testStatisticsMock.Setup(a => a.GetUserTestWrongAnswerCount("1", 1)).Returns(6);

            var result = this._controller.GetWrongAnswerCount("1", 1);

            Assert.True(result == 6, "Temoporary test for ans count - wrong!");
        }
    }
}