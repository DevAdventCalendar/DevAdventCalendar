using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevAdventCalendarCompetition.Repository;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Options;
using DevAdventCalendarCompetition.Services.Profiles;
using DevAdventCalendarCompetition.TestResultService;
using FluentAssertions;
using Xunit;
using static DevAdventCalendarCompetition.Tests.TestHelper;

namespace DevAdventCalendarCompetition.Tests.IntegrationTests
{
    public class ResultsServiceTests : IntegrationTestBase
    {
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

                results[0].Points.Should().Be(30);
                results[0].TotalTime.Should().BeGreaterThan(0);
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

                results[0].Points.Should().Be(70);
                results[0].Position.Should().Be(1);
                results[0].TotalTime.Should().BeGreaterThan(0);
            }
        }

        [Fact]
        public void GetTestResultsForSpecificWeek_ShouldReturnAnswersOnlyForThatWeek()
        {
            var userResult = GetUserWeek2Result();
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
                var results = resultsService.GetTestResults(2, 50, 1);

                results.Count.Should().Be(1);
                string.Equals(results[0].UserName, TestUserName, StringComparison.Ordinal);

                results[0].CorrectAnswersCount.Should().Be(1);
            }
        }

        [Fact]
        public void GetTestResultsForSpecificWeek_ShouldReturnWrongAnswersOnlyForTestsStartedThatWeek()
        {
            var userResult = GetUserWeek2Result();
            var wrongAnswers = GetUserWrongAnswers();

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.Results.Add(userResult);
                context.UserTestWrongAnswers.AddRange(wrongAnswers);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var resultsService = PrepareSUT(context);
                var results = resultsService.GetTestResults(2, 50, 1);

                results.Count.Should().Be(1);
                string.Equals(results[0].UserName, TestUserName, StringComparison.Ordinal);

                results[0].WrongAnswersCount.Should().Be(0);
            }
        }

        [Fact]
        public void GetTestResultsForEveryRanking_ShouldReturnOnlyResultsForEveryRanking()
        {
            var userResult = GetUserFullResult();
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
                var week1Results = resultsService.GetTestResults(1, 50, 1);
                var week2Results = resultsService.GetTestResults(2, 50, 1);
                var week3Results = resultsService.GetTestResults(3, 50, 1);
                var finalResults = resultsService.GetTestResults(4, 50, 1);

                week1Results.Count.Should().Be(1);
                week2Results.Count.Should().Be(1);
                week3Results.Count.Should().Be(1);
                finalResults.Count.Should().Be(1);

                string.Equals(week1Results[0].UserName, TestUserName, StringComparison.Ordinal);
                string.Equals(week2Results[0].UserName, TestUserName, StringComparison.Ordinal);
                string.Equals(week3Results[0].UserName, TestUserName, StringComparison.Ordinal);
                string.Equals(finalResults[0].UserName, TestUserName, StringComparison.Ordinal);

                week1Results[0].Points.Should().Be(30);
                week2Results[0].Points.Should().Be(40);
                week3Results[0].Points.Should().Be(50);
                finalResults[0].Points.Should().Be(120);

                week1Results[0].Position.Should().Be(1);
                week2Results[0].Position.Should().Be(6);
                week3Results[0].Position.Should().Be(12);
                finalResults[0].Position.Should().Be(20);

                week1Results[0].TotalTime.Should().BeGreaterThan(0);
                week2Results[0].TotalTime.Should().BeGreaterThan(0);
                week3Results[0].TotalTime.Should().BeGreaterThan(0);
                finalResults[0].TotalTime.Should().BeGreaterThan(0);
            }
        }

        private static ResultsService PrepareSUT(ApplicationDbContext context)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddMaps(typeof(TestProfile))).CreateMapper();
            var resultsRepository = new ResultsRepository(context);
            var testAnswerRepository = new UserTestAnswersRepository(context);
            return new ResultsService(resultsRepository, testAnswerRepository, mapper, GetTestSettings(), GetAdventSettings());
        }

        private static Result GetUserWeek2Result() => new Result
        {
            UserId = TestUserId,
            Week2Points = 30,
            Week2Place = 1
        };

        private static Result GetUserFullResult() => new Result
        {
            UserId = TestUserId,
            Week1Points = 30,
            Week1Place = 1,
            Week2Points = 40,
            Week2Place = 6,
            Week3Points = 50,
            Week3Place = 12,
            FinalPoints = 120,
            FinalPlace = 20,
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
                new UserTestCorrectAnswer() { UserId = TestUserId, Test = testList[0], AnsweringTime = new DateTime(2020, 12, 2, 20, 0, 0), AnsweringTimeOffset = testList[0].StartDate.Value.Subtract(new DateTime(2020, 12, 2, 20, 0, 0)) * (-1) },
                new UserTestCorrectAnswer() { UserId = TestUserId, Test = testList[1], AnsweringTime = new DateTime(2020, 12, 9, 20, 0, 0), AnsweringTimeOffset = testList[1].StartDate.Value.Subtract(new DateTime(2020, 12, 9, 20, 0, 0)) * (-1) },
                new UserTestCorrectAnswer() { UserId = TestUserId, Test = testList[2], AnsweringTime = new DateTime(2020, 12, 16, 20, 0, 0), AnsweringTimeOffset = testList[2].StartDate.Value.Subtract(new DateTime(2020, 12, 16, 20, 0, 0)) * (-1) },
                new UserTestCorrectAnswer() { UserId = TestUserId, Test = testList[3], AnsweringTime = new DateTime(2020, 12, 24, 20, 0, 0), AnsweringTimeOffset = testList[3].StartDate.Value.Subtract(new DateTime(2020, 12, 24, 20, 0, 0)) * (-1) },
                new UserTestCorrectAnswer() { UserId = TestUserId, Test = testList[4], AnsweringTime = new DateTime(2020, 12, 14, 20, 0, 0), AnsweringTimeOffset = testList[4].StartDate.Value.Subtract(new DateTime(2020, 12, 14, 20, 0, 0)) * (-1) }
            };
        }

        private static List<UserTestWrongAnswer> GetUserWrongAnswers()
        {
            var testList = GetTestList();

            return new List<UserTestWrongAnswer>()
            {
                new UserTestWrongAnswer() { UserId = TestUserId, Test = testList[4], Time = new DateTime(2020, 12, 2, 20, 0, 0), Answer = "Test1234" },
                new UserTestWrongAnswer() { UserId = TestUserId, Test = testList[4], Time = new DateTime(2020, 12, 9, 20, 0, 0), Answer = "Test12345" }
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

        private static AdventSettings GetAdventSettings()
        {
            return new AdventSettings
            {
                StartDate = new DateTime(2020, 12, 1),
                EndDate = new DateTime(2020, 12, 24)
            };
        }
    }
}
