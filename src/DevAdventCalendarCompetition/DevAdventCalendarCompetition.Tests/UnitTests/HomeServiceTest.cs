// This file isn't generated, but this comment is necessary to exclude it from StyleCop analysis. // <auto-generated/>
using System.Collections.Generic;
using AutoFixture.Xunit2;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Models;
using Moq;
using Xunit;
using static DevAdventCalendarCompetition.Tests.TestHelper;

namespace DevAdventCalendarCompetition.Tests.UnitTests
{
    public class HomeServiceTest
    {
        [Theory]
        [AutoMoqData]
        public void GetTestAnswerByUserId_ReturnTestAnswerDto(
            UserTestCorrectAnswer testAnswer,
            [Frozen]Mock<IUserTestAnswersRepository> testAnswerRepositoryMock,
            HomeService homeService)
        {
            // Arrange
            testAnswerRepositoryMock.Setup(mock => mock.GetCorrectAnswerByUserId(It.IsAny<string>(), It.IsAny<int>())).Returns(testAnswer);

            // Act
            var result = homeService.GetCorrectAnswerByUserId(testAnswer.UserId, testAnswer.Id);

            // Assert
            Assert.IsType<UserTestCorrectAnswerDto>(result);
        }

        [Theory]
        [AutoMoqData]
        public void GetTestsWithUserAnswers_ReturnTestWithAnswerListDto(
            List<Test> tests,
            [Frozen]Mock<ITestRepository> testRepositoryMock,
            HomeService homeService)
        {
            // Arrange
            testRepositoryMock.Setup(mock => mock.GetTestsWithUserAnswers()).Returns(tests);

            // Act
            var result = homeService.GetTestsWithUserAnswers();

            // Assert
            Assert.IsType<List<TestWithUserCorrectAnswerListDto>>(result);
            Assert.True(tests.Count == result.Count);
        }
    }
}