using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DevAdventCalendarCompetition.Repository;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Models;
using DevAdventCalendarCompetition.Services.Profiles;
using FluentAssertions;
using Xunit;
using static DevAdventCalendarCompetition.Tests.TestHelper;

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
        public void Gets_previous_test()
        {
            var testList = GetTestList();
            var currentTestNumber = 2;
            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.AddRange(testList);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var adminService = PrepareSUT(context);

                var result = adminService.GetPreviousTest(currentTestNumber);

                result.Should().BeOfType<TestDto>();
                result.Number.Should().Be(1);
                result.StartDate.Should().Be(DateTime.Today.AddDays(-1).AddHours(12));
                result.EndDate.Should().Be(DateTime.Today.AddDays(-1).AddHours(23).AddMinutes(59));
            }
        }

        [Fact]
        public void Adds_correct_amount_of_answers()
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

        [Fact]
        public void Updates_test_end_date()
        {
            var test = GetTest();
            var newDate = DateTime.Today.AddHours(19).AddMinutes(48);
            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.Tests.Add(test);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var adminService = PrepareSUT(context);
                adminService.UpdateTestEndDate(test.Id, newDate);
            }

            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                var result = context.Tests.SingleOrDefault(t => t.Id == test.Id);
                result.EndDate.Should().Be(newDate);
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
    }
}
