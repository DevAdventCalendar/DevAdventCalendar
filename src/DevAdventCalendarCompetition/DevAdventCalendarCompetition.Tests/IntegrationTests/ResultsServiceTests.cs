using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DevAdventCalendarCompetition.Repository;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services;
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
                var result = resultsService.GetAllTestResults();

                result.Count.Should().Be(4);
            }
        }

        private static ResultsService PrepareSUT(ApplicationDbContext context)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddMaps(typeof(TestProfile))).CreateMapper();
            var resultsRepository = new ResultsRepository(context);
            var testAnswerRepository = new UserTestAnswersRepository(context);
            return new ResultsService(resultsRepository, testAnswerRepository, mapper);
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
    }
}
