using AutoMapper;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Models;
using DevAdventCalendarCompetition.Services.Profiles;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace DevAdventCalendarCompetition.Tests
{
    public class HomeServiceTest
    {
        private readonly Mock<IHomeRepository> _homeRepositoryMock;
        private IMapper _mapper;

        private Test _currentTest = new Test()
        {
            Id = 1,
            Number = 1,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(1),
            Answers = null
        };

        private TestAnswer _testAnswer = new TestAnswer()
        {
            Id = 1,
            Test = new Test(),
            TestId = 1,
            User = new ApplicationUser(),
            UserId = "1",
            AnsweringTimeOffset = new TimeSpan(),
            AnsweringTime = DateTime.Today
        };

        private List<Test> _testList = new List<Test>()
        {
            new Test()
            {
                Id = 1,
                Number = 1,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                Answers = null
            }
        };

        private Test _oldTest = new Test()
        {
            Id = 1,
            Number = 1,
            StartDate = DateTime.Today.AddDays(-2),
            EndDate = DateTime.Today.AddDays(-1),
            Answers = null
        };

        private Test _futureTest = new Test()
        {
            Id = 1,
            Number = 1,
            StartDate = DateTime.Today.AddDays(1),
            EndDate = DateTime.Today.AddDays(2),
            Answers = null
        };

        public HomeServiceTest()
        {
            _homeRepositoryMock = new Mock<IHomeRepository>();
        }

        [Fact]
        public void GetCurrentTest_ReturnCurrentTestDto()
        {
            //Arrange
            _homeRepositoryMock.Setup(mock => mock.GetCurrentTest()).Returns(_currentTest);
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var homeService = new HomeService(_homeRepositoryMock.Object, _mapper);
            //Act
            var result = homeService.GetCurrentTest();
            //Assert
            Assert.IsType<TestDto>(result);
            Assert.Equal(result.Id, _currentTest.Id);
            _homeRepositoryMock.Verify(mock => mock.GetCurrentTest(), Times.Once());
        }

        [Fact]
        public void GetCurrentTest_DontGetOldTest()
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
            _homeRepositoryMock.Verify(mock => mock.GetTestAnswerByUserId(It.Is<string>(x=>x.Equals(_testAnswer.UserId)),It.Is<int>(x=>x.Equals(_testAnswer.Id))), Times.Once());
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
            Assert.True(1 == result.Count);
            _homeRepositoryMock.Verify(mock => mock.GetTestsWithUserAnswers(), Times.Once());
        }
    }
}
