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
        private readonly Mock<IHomeRepository> _homeRepositoryMock;

        private readonly Test _currentTest = new Test()
        {
            Id = 2,
            Number = 2,
            StartDate = DateTime.Today.AddHours(12),
            EndDate = DateTime.Today.AddHours(23).AddMinutes(59),
            Answers = null
        };

        private IMapper _mapper;

        private TestAnswer _testAnswer = new TestAnswer()
        {
            Id = 1,
            Test = new Test(),
            TestId = 1,
            User = new ApplicationUser(),
            UserId = "1",
            AnsweringTimeOffset = default(TimeSpan),
            AnsweringTime = DateTime.Now
        };

        private List<Test> _testList = new List<Test>()
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

        private Test _oldTest = new Test()
        {
            Id = 1,
            Number = 1,
            StartDate = DateTime.Today.AddDays(-2).AddHours(12),
            EndDate = DateTime.Today.AddDays(-2).AddHours(23).AddMinutes(59),
            Answers = null
        };

        private Test _futureTest = new Test()
        {
            Id = 3,
            Number = 3,
            StartDate = DateTime.Today.AddDays(1).AddHours(12),
            EndDate = DateTime.Today.AddDays(1).AddHours(23).AddMinutes(59),
            Answers = null
        };

        public HomeServiceTest()
        {
            this._homeRepositoryMock = new Mock<IHomeRepository>();
        }

        [Fact]
        public void GetCurrentTestReturnCurrentTestDto()
        {
            // Arrange
            this._homeRepositoryMock.Setup(mock => mock.GetCurrentTest()).Returns(this._currentTest);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var homeService = new HomeService(this._homeRepositoryMock.Object, this._mapper);

            // Act
            var result = homeService.GetCurrentTest();

            // Assert
            Assert.IsType<TestDto>(result);
            Assert.Equal(result.Id, this._currentTest.Id);
            this._homeRepositoryMock.Verify(mock => mock.GetCurrentTest(), Times.Once());
        }

        [Fact]
        public void GetCurrentTestDontGetOldTest()
        {
            // Arrange
            this._homeRepositoryMock.Setup(mock => mock.GetCurrentTest()).Returns(this._oldTest);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var homeService = new HomeService(this._homeRepositoryMock.Object, this._mapper);

            // Act
            var result = homeService.GetCurrentTest();

            // Assert
            Assert.Null(result);
            this._homeRepositoryMock.Verify(mock => mock.GetCurrentTest(), Times.Once());
        }

        [Fact]
        public void GetCurrentTestDontGetFutureTest()
        {
            // Arrange
            this._homeRepositoryMock.Setup(mock => mock.GetCurrentTest()).Returns(this._futureTest);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var homeService = new HomeService(this._homeRepositoryMock.Object, this._mapper);

            // Act
            var result = homeService.GetCurrentTest();

            // Assert
            Assert.Null(result);
            this._homeRepositoryMock.Verify(mock => mock.GetCurrentTest(), Times.Once());
        }

        [Fact]
        public void GetTestAnswerByUserIdReturnTestAnswerDto()
        {
            // Arrange
            this._homeRepositoryMock.Setup(mock => mock.GetTestAnswerByUserId(It.IsAny<string>(), It.IsAny<int>())).Returns(this._testAnswer);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestAnswerProfile>()).CreateMapper();
            var homeService = new HomeService(this._homeRepositoryMock.Object, this._mapper);

            // Act
            var result = homeService.GetTestAnswerByUserId(this._testAnswer.UserId, this._testAnswer.Id);

            // Assert
            Assert.IsType<TestAnswerDto>(result);
            this._homeRepositoryMock.Verify(mock => mock.GetTestAnswerByUserId(It.Is<string>(x => x.Equals(this._testAnswer.UserId, StringComparison.Ordinal)), It.Is<int>(x => x.Equals(this._testAnswer.Id))), Times.Once());
        }

        [Fact]
        public void GetTestsWithUserAnswersReturnTestWithAnswerListDto()
        {
            // Arrange
            this._homeRepositoryMock.Setup(mock => mock.GetTestsWithUserAnswers()).Returns(this._testList);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var homeService = new HomeService(this._homeRepositoryMock.Object, this._mapper);

            // Act
            var result = homeService.GetTestsWithUserAnswers();

            // Assert
            Assert.IsType<List<TestWithAnswerListDto>>(result);
            Assert.True(this._testList.Count == result.Count);
            this._homeRepositoryMock.Verify(mock => mock.GetTestsWithUserAnswers(), Times.Once());
        }
    }
}