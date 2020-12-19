using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AutoMapper;
using DevAdventCalendarCompetition.Repository;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Options;
using DevAdventCalendarCompetition.Services.Profiles;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using static DevAdventCalendarCompetition.Tests.TestHelper;

namespace DevAdventCalendarCompetition.Tests.IntegrationTests
{
    public class ResultsServiceTests : IntegrationTestBase
    {
        [Fact]
        public void GetAllTestResults_GetsAllTestResults()
        {
            var userResult = GetUserResults();
            var correctAnswers = GetUserCorrectAnswers();

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.Results.Add(userResult);
                context.UserTestCorrectAnswers.AddRange(correctAnswers);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var resultsService = PrepareSUT(context);
                var result = resultsService.GetAllTestResults();

                result.Count.Should().Be(4);
                foreach (var key in result.Keys)
                {
                    result[key].ForEach(x => string.Equals(x.UserName, TestUserName, StringComparison.Ordinal));
                }
            }
        }

        [Fact]
        public void GetTestResultsForWeek_ShouldReturnOnlyResultsForSecondWeek()
        {
            var userResult = GetUserWeek2Results();
            var correctAnswers = GetUserCorrectAnswers();
            const int weekNumber = 2;

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.Results.Add(userResult);
                context.UserTestCorrectAnswers.AddRange(correctAnswers);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var resultsService = PrepareSUT(context);
                var result = resultsService.GetTestResults(weekNumber);

                result.Count.Should().Be(1);
                string.Equals(result[0].UserName, TestUserName, StringComparison.Ordinal);

                result[0].Week1Points.Should().BeNull();
                result[0].Week2Points.Should().Be(30);
                result[0].Week3Points.Should().BeNull();
                result[0].FinalPoints.Should().BeNull();
            }
        }

        [Fact]
        public void GetFinalTestResults_ShouldReturnOnlyFinalTestResults()
        {
            var userResult = GetUserFinalResults();
            var correctAnswers = GetUserCorrectAnswers();

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.Results.Add(userResult);
                context.UserTestCorrectAnswers.AddRange(correctAnswers);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var resultsService = PrepareSUT(context);
                var result = resultsService.GetTestResults();

                result.Count.Should().Be(1);
                string.Equals(result[0].UserName, TestUserName, StringComparison.Ordinal);

                result[0].Week1Points.Should().BeNull();
                result[0].Week2Points.Should().BeNull();
                result[0].Week3Points.Should().BeNull();
                result[0].FinalPoints.Should().Be(70);
            }
        }

        private static ResultsService PrepareSUT(ApplicationDbContext context)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddMaps(typeof(TestProfile))).CreateMapper();
            var resultsRepository = new ResultsRepository(context);
            var testAnswerRepository = new UserTestAnswersRepository(context);
            return new ResultsService(resultsRepository, testAnswerRepository, mapper, GetTestSettings());
        }

        private static Result GetUserResults() => new Result
        {
            UserId = TestUserId,
            Week1Points = 20,
            Week2Points = 30,
            Week3Points = 20,
            Week1Place = 1,
            Week2Place = 1,
            Week3Place = 1,
            FinalPoints = 70,
            FinalPlace = 1
        };

        private static Result GetUserWeek2Results() => new Result
        {
            UserId = TestUserId,
            Week2Points = 30,
            Week2Place = 1,
        };

        private static Result GetUserFinalResults() => new Result
        {
            UserId = TestUserId,
            FinalPoints = 70,
            FinalPlace = 1
        };

        private static List<UserTestCorrectAnswer> GetUserCorrectAnswers()
        {
            var testList = GetTestList();

            return new List<UserTestCorrectAnswer>()
            {
                new UserTestCorrectAnswer() { UserId = TestUserId, Test = testList[0], AnsweringTime = new DateTime(2020, 12, 2, 20, 0, 0) },
                new UserTestCorrectAnswer() { UserId = TestUserId, Test = testList[1], AnsweringTime = new DateTime(2020, 12, 9, 20, 0, 0) },
                new UserTestCorrectAnswer() { UserId = TestUserId, Test = testList[2], AnsweringTime = new DateTime(2020, 12, 16, 20, 0, 0) },
                new UserTestCorrectAnswer() { UserId = TestUserId, Test = testList[3], AnsweringTime = new DateTime(2020, 12, 24, 20, 0, 0) },
            };
        }

        private static TestSettings GetTestSettings()
        {
            return new TestSettings()
            {
                StartHour = new TimeSpan(13, 0, 0),
                EndHour = new TimeSpan(23, 59, 59)
            };
        }
    }
}
