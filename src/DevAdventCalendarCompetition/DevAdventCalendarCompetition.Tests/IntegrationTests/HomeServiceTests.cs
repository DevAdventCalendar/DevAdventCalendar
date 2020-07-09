using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DevAdventCalendarCompetition.Repository;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Models;
using DevAdventCalendarCompetition.Services.Profiles;
using FluentAssertions;
using Microsoft.AspNetCore.Server.IIS.Core;
using Xunit;

namespace DevAdventCalendarCompetition.Tests.IntegrationTests
{
    public class HomeServiceTests : IntegrationTestBase
    {
        public HomeServiceTests()
            : base()
        {
        }

        [Fact]
        public void Returns_correctly_mapped_UserTestCorrectAnswerDto()
        {
            var testCorrectAnswer = GetTestAnswer();
            var test = GetTest();
            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.Tests.Add(test);
                context.UserTestCorrectAnswers.Add(testCorrectAnswer);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var homeService = PrepareSUT(context);
                var result = homeService.GetCorrectAnswerByUserId(testCorrectAnswer.UserId, test.Id);

                result.Should().BeOfType<UserTestCorrectAnswerDto>();
                result.UserId.Should().Be(testCorrectAnswer.UserId);
            }
        }

        [Fact]
        public void Gets_Current_Test()
        {
            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var tests = GetTestList();
                context.Tests.AddRange(tests);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var homeService = PrepareSUT(context);

                var result = homeService.GetCurrentTest();

                result.Id.Should().Be(2);
            }
        }

        private static HomeService PrepareSUT(ApplicationDbContext context)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddMaps(typeof(TestProfile))).CreateMapper();
            var testRepository = new TestRepository(context);
            var testAnswerRepository = new UserTestAnswersRepository(context);
            return new HomeService(testAnswerRepository, testRepository, mapper);
        }

        private static Test GetTest(int number = 2) => GetTestList().First(t => t.Number == number);

        private static List<Test> GetTestList() => new List<Test>()
        {
            new Test()
            {
                Id = 1,
                Number = 1,
                StartDate = DateTime.Today.AddDays(-1).AddHours(12),
                EndDate = DateTime.Today.AddDays(-1).AddHours(23).AddMinutes(59),
                HashedAnswers = null
            },
            new Test()
            {
                Id = 2,
                Number = 2,
                StartDate = DateTime.Today.AddHours(12),
                EndDate = DateTime.Today.AddHours(23).AddMinutes(59),
                HashedAnswers = null
            },
            new Test()
            {
                Id = 3,
                Number = 3,
                StartDate = DateTime.Today.AddDays(1).AddHours(12),
                EndDate = DateTime.Today.AddDays(1).AddHours(23).AddMinutes(59),
                HashedAnswers = null
            }
        };

        private static UserTestCorrectAnswer GetTestAnswer() => new UserTestCorrectAnswer()
        {
            Id = 1,
            TestId = 2,
            UserId = "c611530e-bebd-41a9-ace2-951550edbfa0",
            AnsweringTimeOffset = TimeSpan.FromMinutes(2),
            AnsweringTime = DateTime.Now
        };
    }
}
