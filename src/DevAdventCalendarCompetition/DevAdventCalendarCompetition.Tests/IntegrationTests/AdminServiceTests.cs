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

namespace DevAdventCalendarCompetition.Tests.IntegrationTests
{
    public class AdminServiceTests : IntegrationTestBase
    {
        public AdminServiceTests()
            : base()
        {
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
                var adminService = PrepareSUT(context);

                var result = adminService.GetAllTests();

                result.Should().BeOfType<List<TestDto>>();
                result.Count.Should().Be(testList.Count);
            }
        }

        [Fact]
        public void Gets_all_test_answers()
        {
            var testDto = GetTestDto();
            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var adminService = PrepareSUT(context);

                adminService.AddTest(testDto);
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var result = context.Tests.SingleOrDefault(t => t.Id == testDto.Id);
                result.HashedAnswers.Count.Should().Be(3);
            }
        }

        [Fact]
        public void Updates_test_dates()
        {
            var test = GetTest();
            var minutes = 30;
            var minutesString = "30";
            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.Tests.Add(test);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var adminService = PrepareSUT(context);
                adminService.UpdateTestDates(test.Id, minutesString);
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var result = context.Tests.SingleOrDefault(t => t.Id == test.Id);
                result.StartDate.Should().BeCloseTo(DateTime.Now, 1000);
                result.EndDate.Should().BeCloseTo(DateTime.Now.AddMinutes(minutes), 1000);
            }
        }

        private static AdminService PrepareSUT(ApplicationDbContext context)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddMaps(typeof(TestProfile))).CreateMapper();
            var testRepository = new TestRepository(context);
            var testAnswerRepository = new UserTestAnswersRepository(context);
            var stringHasher = new StringHasher(new HashParameters(100, new byte[] { 1, 2 }));
            return new AdminService(testRepository, testAnswerRepository, mapper, stringHasher);
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

        private static TestDto GetTestDto() => new TestDto()
        {
            Id = 1,
            Number = 1,
            Description = "TestDescription",
            Answers = new List<TestAnswerDto>()
            {
                new TestAnswerDto() { Answer = "Answer1" },
                new TestAnswerDto() { Answer = "Answer2" },
                new TestAnswerDto() { Answer = "Answer3" }
            }
        };
    }
}
