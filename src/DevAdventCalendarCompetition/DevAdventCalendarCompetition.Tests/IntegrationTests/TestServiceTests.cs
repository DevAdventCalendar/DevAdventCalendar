using System;
using System.Linq;
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
    public class TestServiceTests : IntegrationTestBase
    {
        public TestServiceTests()
            : base()
        {
        }

        [Fact]
        public void Gets_test_by_number()
        {
            var test = GetTest();
            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.Tests.Add(test);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var testService = PrepareSUT(context);

                var result = testService.GetTestByNumber(test.Number);

                result.Should().BeOfType<TestDto>();
                result.Id.Should().Be(test.Id);
                result.Number.Should().Be(test.Number);
                result.StartDate.Should().Be(test.StartDate);
                result.EndDate.Should().Be(test.EndDate);
            }
        }

        [Fact]
        public void Adds_user_correct_answer()
        {
            var test = GetTest();
            var userId = "c611530e-bebd-41a9-ace2-951550edbfa0";
            var startDate = DateTime.Now;
            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.Tests.Add(test);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var testService = PrepareSUT(context);

                testService.AddTestAnswer(test.Id, userId, startDate);
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var result = context.UserTestCorrectAnswers.SingleOrDefault(t => t.UserId == userId && t.TestId == test.Id);
                result.Should().NotBe(null);
            }
        }

        [Fact]
        public void Adds_user_wrong_answer()
        {
            var test = GetTest();
            var userId = "c611530e-bebd-41a9-ace2-951550edbfa0";
            var startDate = DateTime.Now;
            var wrongAnswer = "Wrong";
            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.Tests.Add(test);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var testService = PrepareSUT(context);

                testService.AddTestWrongAnswer(userId, test.Id, wrongAnswer, startDate);
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var result = context.UserTestWrongAnswers.SingleOrDefault(t => t.UserId == userId && t.TestId == test.Id);
                result.Should().NotBe(null);
                result.Answer.Should().Be(wrongAnswer);
            }
        }

        [Fact]
        public void Gets_user_correct_answer_by_testId()
        {
            var test = GetTest();
            var userId = "c611530e-bebd-41a9-ace2-951550edbfa0";
            var userCorrectAnswer = new UserTestCorrectAnswer()
            {
                Test = test,
                UserId = userId,
            };

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.UserTestCorrectAnswers.Add(userCorrectAnswer);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var testService = PrepareSUT(context);

                var result = testService.GetAnswerByTestId(test.Id);

                result.Should().BeOfType<UserTestCorrectAnswerDto>();
                result.TestId.Should().Be(test.Id);
                result.UserId.Should().Be(userId);
            }
        }

        [Fact]
        public void Gets_user_correct_answer_by_userId()
        {
            var test = GetTest();
            var userId = "c611530e-bebd-41a9-ace2-951550edbfa0";
            var userCorrectAnswer = new UserTestCorrectAnswer()
            {
                Test = test,
                UserId = userId,
            };

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.UserTestCorrectAnswers.Add(userCorrectAnswer);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var testService = PrepareSUT(context);

                var result = testService.GetAnswerByTestId(test.Id);

                result.Should().BeOfType<UserTestCorrectAnswerDto>();
                result.TestId.Should().Be(test.Id);
                result.UserId.Should().Be(userId);
            }
        }

        [Fact]
        public void Has_user_answered_test()
        {
            var test = GetTest();
            var userId = "c611530e-bebd-41a9-ace2-951550edbfa0";
            var userCorrectAnswer = new UserTestCorrectAnswer()
            {
                Test = test,
                UserId = userId,
            };

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.UserTestCorrectAnswers.Add(userCorrectAnswer);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var testService = PrepareSUT(context);

                var result = testService.HasUserAnsweredTest(userId, test.Id);

                result.Should().Be(true);
            }
        }

        private static TestService PrepareSUT(ApplicationDbContext context)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddMaps(typeof(TestProfile))).CreateMapper();
            var testRepository = new TestRepository(context);
            var testAnswerRepository = new UserTestAnswersRepository(context);
            var stringHasher = new StringHasher(new HashParameters(100, new byte[] { 1, 2 }));
            return new TestService(testRepository, testAnswerRepository, mapper, stringHasher);
        }
    }
}
