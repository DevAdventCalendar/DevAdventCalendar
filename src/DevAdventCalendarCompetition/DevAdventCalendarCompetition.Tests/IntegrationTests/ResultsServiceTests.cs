using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DevAdventCalendarCompetition.Repository;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Models;
using DevAdventCalendarCompetition.Services.Options;
using DevAdventCalendarCompetition.Services.Profiles;
using FluentAssertions;
using Xunit;
using static DevAdventCalendarCompetition.Tests.TestHelper;

namespace DevAdventCalendarCompetition.Tests.IntegrationTests
{
    public class ResultsServiceTests : IntegrationTestBase
    {
        [Fact]
        public void GetAllTestResults_GetsAllTestResults()
        {
            var userResult = GetUserResult();
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
                var allResults = new TestResultDto[4];

                for (int i = 1; i <= 4; i++)
                {
                    allResults[i - 1] = resultsService.GetTestResults(i, 50, 1).FirstOrDefault();
                    allResults[i - 1].Should().NotBeNull();
                }

                allResults[0].Week1Points.Should().Be(20);
                allResults[1].Week2Points.Should().Be(30);
                allResults[2].Week3Points.Should().Be(20);
                allResults[3].FinalPoints.Should().Be(70);
            }
        }

        [Fact]
        public void GetTestResultsForSecondWeek_ShouldReturnOnlyResultsForSecondWeek()
        {
            var userResult = GetUserWeek2Result();
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
                var results = resultsService.GetTestResults(weekNumber, 50, 1);

                results.Count.Should().Be(1);
                string.Equals(results[0].UserName, TestUserName, StringComparison.Ordinal);

                results[0].Week1Points.Should().BeNull();
                results[0].Week2Points.Should().Be(30);
                results[0].Week3Points.Should().BeNull();
                results[0].FinalPoints.Should().BeNull();
            }
        }

        [Fact]
        public void GetFinalTestResults_ShouldReturnOnlyFinalTestResults()
        {
            var userResult = GetUserFinalResult();
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
                var results = resultsService.GetTestResults(4, 50, 1);

                results.Count.Should().Be(1);
                string.Equals(results[0].UserName, TestUserName, StringComparison.Ordinal);

                results[0].Week1Points.Should().BeNull();
                results[0].Week2Points.Should().BeNull();
                results[0].Week3Points.Should().BeNull();
                results[0].FinalPoints.Should().Be(70);
            }
        }

        private static ResultsService PrepareSUT(ApplicationDbContext context)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddMaps(typeof(TestProfile))).CreateMapper();
            var resultsRepository = new ResultsRepository(context);
            var testAnswerRepository = new UserTestAnswersRepository(context);
            return new ResultsService(resultsRepository, testAnswerRepository, mapper, GetTestSettings());
        }

        private static Result GetUserResult() => new Result
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

        private static Result GetUserWeek2Result() => new Result
        {
            UserId = TestUserId,
            Week2Points = 30,
            Week2Place = 1,
        };

        private static Result GetUserFinalResult() => new Result
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
