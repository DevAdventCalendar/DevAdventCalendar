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
    public class BaseTestServiceTest
    {
        private readonly Mock<IBaseTestRepository> _baseTestRepositoryMock;
        private IMapper _mapper;

        private Test oldTest = new Test()
        {
            Id = 1,
            Number = 1,
            StartDate = DateTime.Today.AddDays(-2),
            EndDate = DateTime.Today.AddDays(-1),
            Answers = null
        };

        private Test futureTest = new Test()
        {
            Id = 1,
            Number = 1,
            StartDate = DateTime.Today.AddDays(1),
            EndDate = DateTime.Today.AddDays(2),
            Answers = null
        };

        private TestAnswer testAnswer = new TestAnswer()
        {
            Id = 1,
            Test = new Test(),
            TestId = 1,
            User = new ApplicationUser(),
            UserId = "1",
            AnsweringTimeOffset = new TimeSpan(),
            AnsweringTime = DateTime.Today
        };

        public BaseTestServiceTest()
        {
            _baseTestRepositoryMock = new Mock<IBaseTestRepository>();
        }

        [Fact]
        public void GetTestByNumber_DontGetOldTest_ReturnsCorrectResult()
        {
            //Arrange
            _baseTestRepositoryMock.Setup(mock => mock.GetByNumber(It.IsAny<int>())).Returns(oldTest);
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var baseTestService = new BaseTestService(_baseTestRepositoryMock.Object, _mapper);
            //Act
            var result = baseTestService.GetTestByNumber(oldTest.Number);
            //Assert
            Assert.Null(result);
            _baseTestRepositoryMock.Verify(mock => mock.GetByNumber(oldTest.Number), Times.Once);
        }

        [Fact]
        public void GetTestByNumber_DontGetFutureTest_ReturnsCorrectResult()
        {
            //Arange
            _baseTestRepositoryMock.Setup(mock => mock.GetByNumber(It.IsAny<int>())).Returns(futureTest);
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var baseTestService = new BaseTestService(_baseTestRepositoryMock.Object, _mapper);
            //Act
            var result = baseTestService.GetTestByNumber(futureTest.Number);
            //Assert
            Assert.Null(result);
            _baseTestRepositoryMock.Verify(mock => mock.GetByNumber(futureTest.Number), Times.Once());
        }

        [Fact]
        public void GetTestByNumber_ReturnTestDto_ReturnsCorrectResult()
        {
            //Arrange
            _baseTestRepositoryMock.Setup(mock => mock.GetByNumber(It.IsAny<int>())).Returns(oldTest);
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var baseTestService = new BaseTestService(_baseTestRepositoryMock.Object, _mapper);
            //Act
            var result = baseTestService.GetTestByNumber(oldTest.Number);
            //Assert
            Assert.IsType<TestDto>(result);
        }

        [Fact]
        public void GetAnswerByTestId_ReturnTestAnswerDto_ReturnsCorrectResult()
        {
            //Arrange
            _baseTestRepositoryMock.Setup(mock => mock.GetAnswerByTestId(It.IsAny<int>())).Returns(testAnswer);
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestAnswerProfile>()).CreateMapper();
            var baseTestService = new BaseTestService(_baseTestRepositoryMock.Object, _mapper);
            //Act
            var result = baseTestService.GetAnswerByTestId(testAnswer.Id);
            //Assert
            Assert.IsType<TestAnswerDto>(result);
        }
    }
}