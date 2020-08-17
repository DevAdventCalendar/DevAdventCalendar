using System.Collections.Generic;
using AutoMapper;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Models;
using DevAdventCalendarCompetition.Services.Profiles;
using Moq;
using Xunit;
using static DevAdventCalendarCompetition.Tests.TestHelper;

namespace DevAdventCalendarCompetition.Tests.UnitTests
{
    public class HomeServiceTest
    {
        private readonly Mock<ITestRepository> _testRepositoryMock;
        private readonly Mock<IUserTestAnswersRepository> _testAnswerRepositoryMock;
        private IMapper _mapper;

        public HomeServiceTest()
        {
            this._testRepositoryMock = new Mock<ITestRepository>();
            this._testAnswerRepositoryMock = new Mock<IUserTestAnswersRepository>();
        }

        [Fact]
        public void GetTestAnswerByUserId_ReturnTestAnswerDto()
        {
            // Arrange
            var testAnswer = GetTestAnswer();
            this._testAnswerRepositoryMock.Setup(mock => mock.GetCorrectAnswerByUserId(It.IsAny<string>(), It.IsAny<int>())).Returns(testAnswer);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestAnswerProfile>()).CreateMapper();
            var homeService = new HomeService(this._testAnswerRepositoryMock.Object, this._testRepositoryMock.Object, this._mapper, null);

            // Act
            var result = homeService.GetCorrectAnswerByUserId(testAnswer.UserId, testAnswer.Id);

            // Assert
            Assert.IsType<UserTestCorrectAnswerDto>(result);
        }

        [Fact]
        public void GetTestsWithUserAnswers_ReturnTestWithAnswerListDto()
        {
            // Arrange
            var testList = GetTestList();
            this._testRepositoryMock.Setup(mock => mock.GetTestsWithUserAnswers()).Returns(testList);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var homeService = new HomeService(this._testAnswerRepositoryMock.Object, this._testRepositoryMock.Object, this._mapper, null);

            // Act
            var result = homeService.GetTestsWithUserAnswers();

            // Assert
            Assert.IsType<List<TestWithUserCorrectAnswerListDto>>(result);
            Assert.True(testList.Count == result.Count);
        }
    }
}