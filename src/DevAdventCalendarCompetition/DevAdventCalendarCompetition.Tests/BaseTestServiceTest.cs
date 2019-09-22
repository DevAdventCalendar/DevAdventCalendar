using System;
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
    public class BaseTestServiceTest
    {
        private readonly Mock<IBaseTestRepository> _baseTestRepositoryMock;
        private IMapper _mapper;

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

        private Test _currentTest = new Test()
        {
            Id = 2,
            Number = 2,
            StartDate = DateTime.Today.AddHours(12),
            EndDate = DateTime.Today.AddHours(23).AddMinutes(59),
            Answers = null
        };

        private TestAnswer _testAnswer = new TestAnswer()
        {
            Id = 1,
            Test = new Test(),
            TestId = 1,
            User = new ApplicationUser(),
            UserId = "1",
            AnsweringTimeOffset = default(TimeSpan),
            AnsweringTime = DateTime.Today
        };

        private TestWrongAnswer _testWrongAnswer = new TestWrongAnswer()
        {
            Id = 1,
            Test = new Test(),
            TestId = 1,
            User = new ApplicationUser(),
            UserId = "1",
            Time = DateTime.Today,
            Answer = "abcd"
        };

        public BaseTestServiceTest()
        {
            this._baseTestRepositoryMock = new Mock<IBaseTestRepository>();
        }

        [Fact]
        public void GetTestByNumberDontGetOldTest()
        {
            // Arrange
            this._baseTestRepositoryMock.Setup(mock => mock.GetByNumber(It.IsAny<int>())).Returns(this._oldTest);

            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var baseTestService = new BaseTestService(this._baseTestRepositoryMock.Object, this._mapper, null);

            // Act
            var result = baseTestService.GetTestByNumber(this._oldTest.Number);

            // Assert
            Assert.IsType<TestDto>(result);
            this._baseTestRepositoryMock.Verify(mock => mock.GetByNumber(It.Is<int>(x => x.Equals(this._oldTest.Number))), Times.Once);
        }

        [Fact]
        public void GetTestByNumberDontGetFutureTest()
        {
            // Arange
            this._baseTestRepositoryMock.Setup(mock => mock.GetByNumber(It.IsAny<int>())).Returns(this._futureTest);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var baseTestService = new BaseTestService(this._baseTestRepositoryMock.Object, this._mapper, null);

            // Act
            var result = baseTestService.GetTestByNumber(this._futureTest.Number);

            // Assert
            Assert.IsType<TestDto>(result);
            this._baseTestRepositoryMock.Verify(mock => mock.GetByNumber(It.Is<int>(x => x.Equals(this._futureTest.Number))), Times.Once());
        }

        [Fact]
        public void GetTestByNumberReturnCurrentTestDto()
        {
            // Arrange
            this._baseTestRepositoryMock.Setup(mock => mock.GetByNumber(It.IsAny<int>())).Returns(this._currentTest);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var baseTestService = new BaseTestService(this._baseTestRepositoryMock.Object, this._mapper, null);

            // Act
            var result = baseTestService.GetTestByNumber(this._currentTest.Number);

            // Assert
            Assert.IsType<TestDto>(result);
            this._baseTestRepositoryMock.Verify(mock => mock.GetByNumber(It.Is<int>(x => x.Equals(this._currentTest.Number))), Times.Once());
        }

        [Fact]
        public void GetAnswerByTestIdReturnTestAnswerDto()
        {
            // Arrange
            this._baseTestRepositoryMock.Setup(mock => mock.GetAnswerByTestId(It.IsAny<int>())).Returns(this._testAnswer);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestAnswerProfile>()).CreateMapper();
            var baseTestService = new BaseTestService(this._baseTestRepositoryMock.Object, this._mapper, null);

            // Act
            var result = baseTestService.GetAnswerByTestId(this._testAnswer.Id);

            // Assert
            Assert.IsType<TestAnswerDto>(result);
            this._baseTestRepositoryMock.Verify(mock => mock.GetAnswerByTestId(It.Is<int>(x => x.Equals(this._testAnswer.Id))), Times.Once());
        }

        [Fact]
        public void AddTestAnswer()
        {
            // Arrange
            this._baseTestRepositoryMock.Setup(mock => mock.AddAnswer(It.IsAny<TestAnswer>()));
            var baseTestService = new BaseTestService(this._baseTestRepositoryMock.Object, this._mapper, null);

            // Act
            baseTestService.AddTestAnswer(this._testAnswer.TestId, this._testAnswer.UserId, DateTime.Now);

            // Assert
            this._baseTestRepositoryMock.Verify(mock => mock.AddAnswer(It.Is<TestAnswer>(x => x.UserId == this._testAnswer.UserId && x.TestId == this._testAnswer.TestId)), Times.Once());
        }

        [Fact]
        public void AddTestWrongAnswer()
        {
            // Arrange
            this._baseTestRepositoryMock.Setup(mock => mock.AddWrongAnswer(It.IsAny<TestWrongAnswer>()));
            var baseTestService = new BaseTestService(this._baseTestRepositoryMock.Object, this._mapper, null);

            // Act
            baseTestService.AddTestWrongAnswer(this._testWrongAnswer.UserId, this._testWrongAnswer.TestId, this._testWrongAnswer.Answer, this._testWrongAnswer.Time);

            // Assert
            this._baseTestRepositoryMock.Verify(
                mock => mock.AddWrongAnswer(
                    It.Is<TestWrongAnswer>(
                        x => x.UserId == this._testWrongAnswer.UserId
                        && x.Time == this._testWrongAnswer.Time
                        && x.Answer == this._testWrongAnswer.Answer
                        && x.TestId == this._testWrongAnswer.TestId)), Times.Once());
        }
    }
}