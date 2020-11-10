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
using DevAdventCalendarCompetition.Services.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace DevAdventCalendarCompetition.Tests.UnitTests
{
    public class StatisticsServiceTests
    {
        private readonly Mock<IStatisticsRepository> _statisticsRepositoryMock;
        private readonly StatisticsService _statisticsService;

        private readonly List<StatisticsDto> _results = new List<StatisticsDto>
        {
            new StatisticsDto { CorrectAnswerDateTime = DateTime.MinValue.AddDays(1), WrongAnswerCount = 1, TestNumber = 1 },
            new StatisticsDto { CorrectAnswerDateTime = DateTime.MinValue.AddDays(2), WrongAnswerCount = 2, TestNumber = 2 },
            new StatisticsDto { CorrectAnswerDateTime = DateTime.MinValue.AddDays(3), WrongAnswerCount = 3, TestNumber = 3 }
        };

        public StatisticsServiceTests()
        {
            this._statisticsRepositoryMock = new Mock<IStatisticsRepository>();
            this._statisticsService = new StatisticsService(this._statisticsRepositoryMock.Object);
        }

        [Fact]
        public void MakeStatisticsDtoList()
        {
            string userId = "c611530e-bebd-41a9-ace2-951550edbfa0";
            int testId = 5;
            int testNumber = 3;
            this._statisticsRepositoryMock.Setup(a => a.GetHighestTestNumber(testId)).Returns(testNumber);
        }

        [Fact]
        public void GetHighestTestNumberForNoTests()
        {
            string userId = "c611530e-bebd-41a9-ace2-951550edbfa0";
            int noTests = 0;

            this._statisticsRepositoryMock.Setup(a => a.GetAnsweredCorrectMaxTestId(userId)).Returns(0);
            this._statisticsRepositoryMock.Setup(a => a.GetAnsweredWrongMaxTestId(userId)).Returns(0);
            this._statisticsRepositoryMock.Setup(a => a.GetHighestTestNumber(0)).Returns(0);

            var result = this._statisticsService.GetHighestTestNumber(userId);

            Assert.Equal(result, noTests);
        }

        [Fact]
        public void GetHighestTestNumberForNoCorrectAnswers()
        {
            string userId = "c611530e-bebd-41a9-ace2-951550edbfa0";
            int twoWrongAnswers = 2;

            this._statisticsRepositoryMock.Setup(a => a.GetAnsweredCorrectMaxTestId(userId)).Returns(0);
            this._statisticsRepositoryMock.Setup(a => a.GetAnsweredWrongMaxTestId(userId)).Returns(4);
            this._statisticsRepositoryMock.Setup(a => a.GetHighestTestNumber(4)).Returns(2);

            var result = this._statisticsService.GetHighestTestNumber(userId);

            Assert.Equal(result, twoWrongAnswers);
        }

        [Fact]
        public void GetHighestTestNumberForNoWrongAnswers()
        {
            string userId = "c611530e-bebd-41a9-ace2-951550edbfa0";
            int twoCorrectAnswers = 2;

            this._statisticsRepositoryMock.Setup(a => a.GetAnsweredCorrectMaxTestId(userId)).Returns(4);
            this._statisticsRepositoryMock.Setup(a => a.GetAnsweredWrongMaxTestId(userId)).Returns(0);
            this._statisticsRepositoryMock.Setup(a => a.GetHighestTestNumber(4)).Returns(2);

            var result = this._statisticsService.GetHighestTestNumber(userId);

            Assert.Equal(result, twoCorrectAnswers);
        }

        [Fact]
        public void GetHighestTestNumberForSomeWrongAndCorrectAnswers()
        {
            string userId = "c611530e-bebd-41a9-ace2-951550edbfa0";
            int twoCorrectAnswersAreHigher = 2;

            this._statisticsRepositoryMock.Setup(a => a.GetAnsweredCorrectMaxTestId(userId)).Returns(4);
            this._statisticsRepositoryMock.Setup(a => a.GetAnsweredWrongMaxTestId(userId)).Returns(3);
            this._statisticsRepositoryMock.Setup(a => a.GetHighestTestNumber(4)).Returns(2);
            this._statisticsRepositoryMock.Setup(a => a.GetHighestTestNumber(3)).Returns(1);

            var result = this._statisticsService.GetHighestTestNumber(userId);

            Assert.Equal(result, twoCorrectAnswersAreHigher);
        }
    }
}