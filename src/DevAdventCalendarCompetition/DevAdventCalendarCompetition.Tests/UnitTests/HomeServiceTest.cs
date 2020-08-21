using System.Collections.Generic;
using AutoFixture.Xunit2;
using DevAdventCalendarCompetition.Repository.Interfaces;
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
        public void GetTestAnswerByUserId_ReturnTestAnswerDto([Frozen]Mock<IUserTestAnswersRepository> testAnswerRepositoryMock, HomeService homeService)
        {
            // Arrange
            var testAnswer = GetTestAnswer();
#pragma warning disable CA1062 // Validate arguments of public methods
            testAnswerRepositoryMock.Setup(mock => mock.GetCorrectAnswerByUserId(It.IsAny<string>(), It.IsAny<int>())).Returns(testAnswer);
#pragma warning restore CA1062 // Validate arguments of public methods

            // Act
#pragma warning disable CA1062 // Validate arguments of public methods
            var result = homeService.GetCorrectAnswerByUserId(testAnswer.UserId, testAnswer.Id);
#pragma warning restore CA1062 // Validate arguments of public methods

            // Assert
            Assert.IsType<UserTestCorrectAnswerDto>(result);
        }

        [Theory]
        [AutoMoqData]
        public void GetTestsWithUserAnswers_ReturnTestWithAnswerListDto([Frozen]Mock<ITestRepository> testRepositoryMock, HomeService homeService)
        {
            // Arrange
            var testList = GetTestList();
#pragma warning disable CA1062 // Validate arguments of public methods
            testRepositoryMock.Setup(mock => mock.GetTestsWithUserAnswers()).Returns(testList);
#pragma warning restore CA1062 // Validate arguments of public methods

            // Act
#pragma warning disable CA1062 // Validate arguments of public methods
            var result = homeService.GetTestsWithUserAnswers();
#pragma warning restore CA1062 // Validate arguments of public methods

            // Assert
            Assert.IsType<List<TestWithUserCorrectAnswerListDto>>(result);
            Assert.True(testList.Count == result.Count);
        }
    }
}