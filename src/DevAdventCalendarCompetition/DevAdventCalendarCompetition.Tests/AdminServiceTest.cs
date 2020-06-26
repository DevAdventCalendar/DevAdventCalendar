using System;
using System.Collections.Generic;
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

namespace DevAdventCalendarCompetition.Tests
{
    public class AdminServiceTest
    {
        private readonly Mock<ITestRepository> _testRepositoryMock;
        private readonly Mock<ITestAnswerRepository> _testAnswerRepositoryMock;
        private IMapper _mapper;

        public AdminServiceTest()
        {
            this._testRepositoryMock = new Mock<ITestRepository>();
            this._testAnswerRepositoryMock = new Mock<ITestAnswerRepository>();
        }

        [Fact]
        public void GetAllTests_ReturnTestDtoList()
        {
            // Arrange
            var testList = GetTestList();
            this._testRepositoryMock.Setup(mock => mock.GetAll()).Returns(testList);
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
            this._testRepositoryMock.Setup(mock => mock.GetById(It.IsAny<int>())).Returns(test);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var adminService = new AdminService(this._testRepositoryMock.Object, this._testAnswerRepositoryMock.Object, this._mapper, null);

            // Act
            var result = adminService.GetTestById(test.Id);

            // Assert
            result.Should().BeOfType<TestDto>();
            result.Number.Should().Be(test.Number);
            result.StartDate.Should().Be(test.StartDate);
            result.EndDate.Should().Be(test.EndDate);
        }

        [Fact]
        public void GetPreviousTest_ReturnPreviousTestDto()
        {
            // Arrange
            var currentTest = GetTest();
            var previousTest = GetPreviousTest();
            var previousTestId = currentTest.Number - 1;
            this._testRepositoryMock.Setup(mock => mock.GetByNumber(previousTestId)).Returns(previousTest);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var adminService = new AdminService(this._testRepositoryMock.Object, this._testAnswerRepositoryMock.Object, this._mapper, null);

            // Act
            var result = adminService.GetPreviousTest(currentTest.Number);

            // Assert
            result.Should().BeOfType<TestDto>();
            result.Number.Should().Be(previousTest.Number);
            result.StartDate.Should().Be(previousTest.StartDate);
            result.EndDate.Should().Be(previousTest.EndDate);
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
            this._testRepositoryMock.Verify(mock => mock.UpdateDates(test.Id, It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once());
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
            this._testRepositoryMock.Verify(mock => mock.UpdateEndDate(test.Id, newDate), Times.Once());
        }

        private static List<Test> GetTestList() => new List<Test>()
        {
            new Test()
            {
                Id = 1,
                Number = 1,
                StartDate = DateTime.Today.AddHours(12),
                EndDate = DateTime.Today.AddHours(23).AddMinutes(59),
                Answers = null
            },
            new Test()
            {
                Id = 2,
                Number = 2,
                StartDate = DateTime.Today.AddHours(10),
                EndDate = DateTime.Today.AddHours(22).AddMinutes(0),
                Answers = null
            }
        };

        private static Test GetTest() => new Test()
        {
            Id = 2,
            Number = 2,
            StartDate = DateTime.Today.AddHours(11).AddMinutes(20),
            EndDate = DateTime.Today.AddHours(23).AddMinutes(50),
            Answers = null
        };

        private static Test GetPreviousTest() => new Test()
        {
            Id = 1,
            Number = 1,
            StartDate = DateTime.Today.AddDays(-2).AddHours(12),
            EndDate = DateTime.Today.AddDays(-2).AddHours(23).AddMinutes(59),
            Answers = null
        };
    }
}