using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AutoMapper;
using DevAdventCalendarCompetition.Repository;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Interfaces;
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
        private readonly Mock<ITestStatisticsRepository> _testStatisticsRepositoryMock;
        private readonly StatisticsService _statisticsService;

        public StatisticsServiceTests()
        {
            this._testStatisticsRepositoryMock = new Mock<ITestStatisticsRepository>();

            this._statisticsService = new StatisticsService(this._testStatisticsRepositoryMock.Object);
        }

        [Fact]
        public void TemporaryTest()
        {
            int a = this._statisticsService.TmpStatisticsImpementation("1", 2);

            Assert.True(a == 3, "Temoporary test - wrong!");
        }

        [Fact]
        public void TemporaryTest2()
        {
            DateTime currentTest = DateTime.Now;
            this._testStatisticsRepositoryMock.Setup(a => a.GetUserTestCorrectAnswerDate("1", 1)).Returns(currentTest);

            var result = this._statisticsService.GetCorrectAnswerDateTime("1", 1);

            Assert.True(result == currentTest, "Temoporary test for datetime - wrong!");
        }

        [Fact]
        public void TemporaryTest3()
        {
            this._testStatisticsRepositoryMock.Setup(a => a.GetUserTestWrongAnswerCount("1", 1)).Returns(6);

            var result = this._statisticsService.GetWrongAnswerCount("1", 1);

            // Assert.True(result == 6, "Temoporary test for ans count - wrong!");
            Assert.Equal(6, result);
        }
    }
}