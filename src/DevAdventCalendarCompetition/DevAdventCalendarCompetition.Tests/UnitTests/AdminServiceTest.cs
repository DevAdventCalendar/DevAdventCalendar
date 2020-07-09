using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AutoMapper;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using DevAdventCalendarCompetition.Services.Profiles;
using FluentAssertions;
using Moq;
using Xunit;

namespace DevAdventCalendarCompetition.Tests.UnitTests
{
    public class AdminServiceTest
    {
        private readonly Mock<ITestRepository> _testRepositoryMock;
        private readonly Mock<IUserTestAnswersRepository> _testAnswerRepositoryMock;
        private IMapper _mapper;

        public AdminServiceTest()
        {
            this._testRepositoryMock = new Mock<ITestRepository>();
            this._testAnswerRepositoryMock = new Mock<IUserTestAnswersRepository>();
        }

        [Fact]
        public void GetAllTests_ReturnTestDtoList()
        {
            // Arrange
            var testList = GetTestList();
            this._testRepositoryMock.Setup(mock => mock.GetAllTests()).Returns(testList);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var adminService = new AdminService(this._testRepositoryMock.Object, this._testAnswerRepositoryMock.Object, this._mapper, null);

            // Act
            var result = adminService.GetAllTests();

            // Assert
            result.Should().BeOfType<List<TestDto>>();
            result.Count.Should().Be(testList.Count);
        }

        [Fact]
        public void GetTestBy_IdReturnTestDto()
        {
            // Arrange
            var test = GetTest();
            this._testRepositoryMock.Setup(mock => mock.GetTestById(It.IsAny<int>())).Returns(test);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var adminService = new AdminService(this._testRepositoryMock.Object, this._testAnswerRepositoryMock.Object, this._mapper, null);

            // Act
            var result = adminService.GetTestById(test.Id);

            // Assert
            result.Should().BeOfType<TestDto>();
            result.Id.Should().Be(test.Id);
            result.Number.Should().Be(test.Number);
            result.StartDate.Should().Be(test.StartDate);
            result.EndDate.Should().Be(test.EndDate);
        }

        [Fact]
        public void GetPreviousTest_ReturnPreviousTestDto()
        {
            // Arrange
            var currentTestNumber = GetTest().Number;
            var previousTestNumber = currentTestNumber - 1;
            var previousTest = GetTest(previousTestNumber);
            this._testRepositoryMock.Setup(mock => mock.GetTestByNumber(previousTestNumber)).Returns(previousTest);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var adminService = new AdminService(this._testRepositoryMock.Object, this._testAnswerRepositoryMock.Object, this._mapper, null);

            // Act
            var result = adminService.GetPreviousTest(currentTestNumber);

            // Assert
            result.Should().BeOfType<TestDto>();
            result.Number.Should().Be(previousTest.Number);
            result.StartDate.Should().Be(previousTest.StartDate);
            result.EndDate.Should().Be(previousTest.EndDate);
        }

        [Fact]
        public void AddTest_AddCorrectAmountOfAnswers()
        {
            // Arrange
            var test = GetTestDto();
            this._testRepositoryMock.Setup(mock => mock.AddTest(It.IsAny<Test>()));
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var stringHasher = new StringHasher(new HashParameters(100, new byte[] { 1, 2 }));
            var adminService = new AdminService(this._testRepositoryMock.Object, this._testAnswerRepositoryMock.Object, this._mapper, stringHasher);

            // Act
            adminService.AddTest(test);

            // Assert
            this._testRepositoryMock.Verify(mock => mock.AddTest(It.Is<Test>(t => t.HashedAnswers.Count == test.Answers.Count)));
        }

        [Fact]
        public void UpdateTestDates()
        {
            // Arrange
            var test = GetTest();
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var adminService = new AdminService(this._testRepositoryMock.Object, this._testAnswerRepositoryMock.Object, this._mapper, null);

            // Act
            adminService.UpdateTestDates(test.Id, "20");

            // Assert
            this._testRepositoryMock.Verify(mock => mock.UpdateTestDates(test.Id, It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once());
        }

        [Fact]
        public void UpdateTestEndDate()
        {
            // Arrange
            var test = GetTest();
            var newDate = DateTime.Today.AddHours(23).AddMinutes(20);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var adminService = new AdminService(this._testRepositoryMock.Object, this._testAnswerRepositoryMock.Object, this._mapper, null);

            // Act
            adminService.UpdateTestEndDate(test.Id, newDate);

            // Assert
            this._testRepositoryMock.Verify(mock => mock.UpdateTestEndDate(test.Id, newDate), Times.Once());
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