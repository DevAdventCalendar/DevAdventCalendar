using System;
using System.Collections.Generic;
using AutoMapper;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Models;
using DevAdventCalendarCompetition.Services.Profiles;
using Moq;
using Xunit;

namespace DevAdventCalendarCompetition.Tests
{
    public class HomeServiceTest
    {
        private readonly Mock<ITestRepository> _testRepositoryMock;
        private readonly Mock<ITestAnswerRepository> _testAnswerRepositoryMock;
        private IMapper _mapper;

        public HomeServiceTest()
        {
            this._testRepositoryMock = new Mock<ITestRepository>();
            this._testAnswerRepositoryMock = new Mock<ITestAnswerRepository>();
        }

        [Fact]
        public void GetCurrentTest_ReturnCurrentTestDto()
        {
            // Arrange
            var currentTest = GetCurrentTest();
            this._testRepositoryMock.Setup(mock => mock.GetCurrentTest()).Returns(currentTest);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var homeService = new HomeService(this._testAnswerRepositoryMock.Object, this._testRepositoryMock.Object, this._mapper);

            // Act
            var result = homeService.GetCurrentTest();

            // Assert
            Assert.IsType<TestDto>(result);
            Assert.Equal(result.Id, currentTest.Id);
        }

        [Fact]
        public void GetCurrentTest_DontGetOldTest()
        {
            // Arrange
            var oldTest = GetOldTest();
            this._testRepositoryMock.Setup(mock => mock.GetCurrentTest()).Returns(oldTest);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var homeService = new HomeService(this._testAnswerRepositoryMock.Object, this._testRepositoryMock.Object, this._mapper);

            // Act
            var result = homeService.GetCurrentTest();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetCurrentTest_DontGetFutureTest()
        {
            // Arrange
            var futureTest = GetFutureTest();
            this._testRepositoryMock.Setup(mock => mock.GetCurrentTest()).Returns(futureTest);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var homeService = new HomeService(this._testAnswerRepositoryMock.Object, this._testRepositoryMock.Object, this._mapper);

            // Act
            var result = homeService.GetCurrentTest();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetTestAnswerByUserId_ReturnTestAnswerDto()
        {
            // Arrange
            var testAnswer = GetTestAnswer();
            this._testAnswerRepositoryMock.Setup(mock => mock.GetTestAnswerByUserId(It.IsAny<string>(), It.IsAny<int>())).Returns(testAnswer);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestAnswerProfile>()).CreateMapper();
            var homeService = new HomeService(this._testAnswerRepositoryMock.Object, this._testRepositoryMock.Object, this._mapper);

            // Act
            var result = homeService.GetTestAnswerByUserId(testAnswer.UserId, testAnswer.Id);

            // Assert
            Assert.IsType<TestAnswerDto>(result);
        }

        [Fact]
        public void GetTestsWithUserAnswers_ReturnTestWithAnswerListDto()
        {
            // Arrange
            var testList = GetTestList();
            this._testRepositoryMock.Setup(mock => mock.GetTestsWithUserAnswers()).Returns(testList);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var homeService = new HomeService(this._testAnswerRepositoryMock.Object, this._testRepositoryMock.Object, this._mapper);

            // Act
            var result = homeService.GetTestsWithUserAnswers();

            // Assert
            Assert.IsType<List<TestWithAnswerListDto>>(result);
            Assert.True(testList.Count == result.Count);
        }

        private static Test GetCurrentTest() => new Test()
        {
            Id = 2,
            Number = 2,
            StartDate = DateTime.Today.AddHours(12),
            EndDate = DateTime.Today.AddHours(23).AddMinutes(59),
            Answers = null
        };

        private static TestAnswer GetTestAnswer() => new TestAnswer()
        {
            Id = 1,
            Test = new Test(),
            TestId = 1,
            User = new ApplicationUser(),
            UserId = "1",
            AnsweringTimeOffset = default,
            AnsweringTime = DateTime.Now
        };

        private static List<Test> GetTestList() => new List<Test>()
        {
            new Test()
            {
                Id = 1,
                Number = 1,
                StartDate = DateTime.Today.AddHours(12),
                EndDate = DateTime.Today.AddHours(23).AddMinutes(59),
                Answers = null
            }
        };

        private static Test GetOldTest() => new Test()
        {
            Id = 1,
            Number = 1,
            StartDate = DateTime.Today.AddDays(-2).AddHours(12),
            EndDate = DateTime.Today.AddDays(-2).AddHours(23).AddMinutes(59),
            Answers = null
        };

        private static Test GetFutureTest() => new Test()
        {
            Id = 3,
            Number = 3,
            StartDate = DateTime.Today.AddDays(1).AddHours(12),
            EndDate = DateTime.Today.AddDays(1).AddHours(23).AddMinutes(59),
            Answers = null
        };
    }
}