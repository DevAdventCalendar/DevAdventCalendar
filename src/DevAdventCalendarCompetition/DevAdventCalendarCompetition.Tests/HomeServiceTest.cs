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
            //Arrange
            _homeRepositoryMock.Setup(mock => mock.GetCurrentTest()).Returns(_oldTest);
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var homeService = new HomeService(_homeRepositoryMock.Object, _mapper);
            //Act
            var result = homeService.GetCurrentTest();
            //Assert
            Assert.Null(result);
            _homeRepositoryMock.Verify(mock => mock.GetCurrentTest(), Times.Once());
        }

        [Fact]
        public void GetCurrentTest_DontGetFutureTest()
        {
            //Arrange
            _homeRepositoryMock.Setup(mock => mock.GetCurrentTest()).Returns(_futureTest);
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var homeService = new HomeService(_homeRepositoryMock.Object, _mapper);
            //Act
            var result = homeService.GetCurrentTest();
            //Assert
            Assert.Null(result);
            _homeRepositoryMock.Verify(mock => mock.GetCurrentTest(), Times.Once());
        }

        [Fact]
        public void GetTestAnswerByUserId_ReturnTestAnswerDto()
        {
            //Arrange
            _homeRepositoryMock.Setup(mock => mock.GetTestAnswerByUserId(It.IsAny<string>(), It.IsAny<int>())).Returns(_testAnswer);
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestAnswerProfile>()).CreateMapper();
            var homeService = new HomeService(_homeRepositoryMock.Object, _mapper);
            //Act
            var result = homeService.GetTestAnswerByUserId(_testAnswer.UserId, _testAnswer.Id);
            //Assert
            Assert.IsType<TestAnswerDto>(result);
            _homeRepositoryMock.Verify(mock => mock.GetTestAnswerByUserId(It.Is<string>(x => x.Equals(_testAnswer.UserId)), It.Is<int>(x => x.Equals(_testAnswer.Id))), Times.Once());
        }

        [Fact]
        public void GetTestsWithUserAnswers_ReturnTestWithAnswerListDto()
        {
            //Arrange
            _homeRepositoryMock.Setup(mock => mock.GetTestsWithUserAnswers()).Returns(_testList);
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var homeService = new HomeService(_homeRepositoryMock.Object, _mapper);
            //Act
            var result = homeService.GetTestsWithUserAnswers();
            //Assert
            Assert.IsType<List<TestWithAnswerListDto>>(result);
            Assert.True(_testList.Count == result.Count);
            _homeRepositoryMock.Verify(mock => mock.GetTestsWithUserAnswers(), Times.Once());
        }
    }
}