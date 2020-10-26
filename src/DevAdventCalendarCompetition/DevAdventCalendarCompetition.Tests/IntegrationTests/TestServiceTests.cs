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

        public static List<object[]> WrongAnswerTestData => new List<object[]>
        {
            new object[] { GetTest(), "Wrong", DateTime.Now }
        };

        [Fact]
        public void GetTestByNumber_GetsTestByNumber()
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
        public void AddTestAnswer_UserAnsweredCorrectly_AddsUserCorrectAnswer()
        {
            var test = GetTest();
            var startDate = DateTime.Now;
            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.Tests.Add(test);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var testService = PrepareSUT(context);

                testService.AddTestAnswer(test.Id, TestUserId, startDate);
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var result = context.UserTestCorrectAnswers.SingleOrDefault(t => t.UserId == TestUserId && t.TestId == test.Id);
                result.Should().NotBe(null);
            }
        }

        [Theory]
        [MemberData(nameof(WrongAnswerTestData))]
        public void AddTestWrongAnswer_UserAnsweredWrongly_AddsUserWrongAnswer(Test test, string wrongAnswer, DateTime startDate)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.Tests.Add(test);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var testService = PrepareSUT(context);

                testService.AddTestWrongAnswer(TestUserId, test.Id, wrongAnswer, startDate);
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var result = context.UserTestWrongAnswers.SingleOrDefault(t => t.UserId == TestUserId && t.TestId == test.Id);
                result.Should().NotBe(null);
                result.Answer.Should().Be(wrongAnswer);
            }
        }

        [Fact]
        public void GetAnswerByTestId_GetsUserCorrectAnswerByTestId()
        {
            var test = GetTest();
            var userCorrectAnswer = new UserTestCorrectAnswer()
            {
                Test = test,
                UserId = TestUserId,
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
                result.UserId.Should().Be(TestUserId);
            }
        }

        [Fact]
        public void HasUserAnsweredTest_UserAnswered_ReturnsTrue()
        {
            var test = GetTest();
            var userCorrectAnswer = new UserTestCorrectAnswer()
            {
                Test = test,
                UserId = TestUserId,
            };

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.UserTestCorrectAnswers.Add(userCorrectAnswer);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var testService = PrepareSUT(context);

                var result = testService.HasUserAnsweredTest(TestUserId, test.Id);

                result.Should().Be(true);
            }
        }

        [Fact]
        public void HasUserAnsweredTest_UserDidNotAnswer_ReturnsFalse()
        {
            var test = GetTest();

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var testService = PrepareSUT(context);

                var result = testService.HasUserAnsweredTest(TestUserId, test.Id);

                result.Should().Be(false);
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
