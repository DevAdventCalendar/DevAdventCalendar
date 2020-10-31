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
        private readonly Mock<IStatisticsRepository> _statisticsRepositoryMock;
        private readonly StatisticsService _statisticsService;

        public StatisticsServiceTests()
        {
            this._statisticsRepositoryMock = new Mock<IStatisticsRepository>();

            this._statisticsService = new StatisticsService(this._statisticsRepositoryMock.Object);
        }

        [Fact]
        public void GetUserTestCorrectAnswerDateResturnsCorrectDateTime()
        {
            DateTime currentTest = DateTime.Now;
            this._statisticsRepositoryMock.Setup(a => a.GetUserTestCorrectAnswerDate("1", 1)).Returns(currentTest);

            var result = this._statisticsService.GetCorrectAnswerDateTime("1", 1);

            Assert.True(result == currentTest, "Temoporary test for datetime - wrong!");
        }

        [Fact]
        public void GetUserTestWrongAnswerCountReturnsCorrectIntiger()
        {
            this._statisticsRepositoryMock.Setup(a => a.GetUserTestWrongAnswerCount("1", 1)).Returns(6);

            var result = this._statisticsService.GetWrongAnswerCount("1", 1);

            // Assert.True(result == 6, "Temoporary test for ans count - wrong!");
            Assert.Equal(6, result);
        }
    }
}