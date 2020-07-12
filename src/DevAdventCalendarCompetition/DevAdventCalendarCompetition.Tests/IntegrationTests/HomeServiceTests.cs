using System.Collections.Generic;
using AutoMapper;
using DevAdventCalendarCompetition.Repository;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Models;
using DevAdventCalendarCompetition.Services.Profiles;
using FluentAssertions;
using Xunit;
using static DevAdventCalendarCompetition.Tests.TestHelper;

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
        public void Gets_all_tests()
        {
            var testList = GetTestList();
            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.AddRange(testList);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var homeService = PrepareSUT(context);

                var result = homeService.GetCurrentTests();

                result.Should().BeOfType<List<TestDto>>();
                result.Count.Should().Be(testList.Count);
            }
        }

        [Fact]
        public void Gets_correct_answers_count_for_user()
        {
            var testList = GetTestList();
            var userId = "c611530e-bebd-41a9-ace2-951550edbfa0";
            var correctAnswers = new List<UserTestCorrectAnswer>()
            {
                new UserTestCorrectAnswer() { UserId = userId, Test = testList[0] },
                new UserTestCorrectAnswer() { UserId = userId, Test = testList[1] }
            };

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.UserTestCorrectAnswers.AddRange(correctAnswers);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var homeService = PrepareSUT(context);
                var result = homeService.GetCorrectAnswersCountForUser(userId);

                result.Should().Be(correctAnswers.Count);
            }
        }

        private static HomeService PrepareSUT(ApplicationDbContext context)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddMaps(typeof(TestProfile))).CreateMapper();
            var testRepository = new TestRepository(context);
            var testAnswerRepository = new UserTestAnswersRepository(context);
            return new HomeService(testAnswerRepository, testRepository, mapper);
        }
    }
}
