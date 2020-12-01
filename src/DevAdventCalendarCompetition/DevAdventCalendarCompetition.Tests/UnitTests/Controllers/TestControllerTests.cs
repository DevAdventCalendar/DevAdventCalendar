using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DevAdventCalendarCompetition.Controllers;
using DevAdventCalendarCompetition.Models.Test;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Resources;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using static DevAdventCalendarCompetition.Resources.ViewsMessages;
using static DevAdventCalendarCompetition.Tests.TestHelper;

namespace DevAdventCalendarCompetition.Tests.UnitTests.Controllers
{
    public class TestControllerTests
    {
        private readonly Mock<ITestService> _testServiceMock;
        private readonly Mock<IAnswerService> _answerServiceMock;

        public TestControllerTests()
        {
            this._testServiceMock = new Mock<ITestService>();
            this._answerServiceMock = new Mock<IAnswerService>();
        }

        [Fact]
        public void Index_GivenStartedTest_UserHasAnsweredCorrectly_ReturnsTestDto()
        {
            // Arrange
            var test = GetTest(TestStatus.Started);
            var answeredCorrectly = true;
            this._testServiceMock.Setup(x => x.GetTestByNumber(test.Id)).Returns(test);
            this._testServiceMock.Setup(x => x.HasUserAnsweredTest(It.IsAny<string>(), test.Id))
                .Returns(answeredCorrectly);
            using var controller = new TestController(this._testServiceMock.Object, this._answerServiceMock.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = null }
            };

            // Act
            var result = controller.Index(test.Id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var testDto = Assert.IsType<TestDto>(viewResult.ViewData.Model);
            testDto.HasUserAnswered.Should().Be(answeredCorrectly);
        }

        [Fact]
        public void Index_GivenStartedTest_UserHasAnsweredWrong_ReturnsViewWithTestDto()
        {
            // Arrange
            var test = GetTest(TestStatus.Started);
            var answeredCorrectly = false;
            this._testServiceMock.Setup(x => x.GetTestByNumber(test.Id)).Returns(test);
            using var controller = new TestController(this._testServiceMock.Object, this._answerServiceMock.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = null }
            };

            // Act
            var result = controller.Index(test.Id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var testDto = Assert.IsType<TestDto>(viewResult.ViewData.Model);
            testDto.HasUserAnswered.Should().Be(answeredCorrectly);
        }

        [Fact]
        public void Index_GivenNotStartedTest_UserTryToNavigateToTestPage_ReturnsNotFound()
        {
            // Arrange
            var test = GetTest(TestStatus.NotStarted);
            this._testServiceMock.Setup(x => x.GetTestByNumber(test.Id)).Returns(test);
            this._testServiceMock.Setup(x => x.HasUserAnsweredTest(It.IsAny<string>(), test.Id)).Returns(false);
            using var controller = new TestController(this._testServiceMock.Object, this._answerServiceMock.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = null }
            };

            // Act
            var result = controller.Index(test.Id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Index_GivenEndedTest_UserTryToNavigateToTestPage_ReturnsViewWithTestDto()
        {
            // Arrange
            var test = GetTest(TestStatus.Ended);
            this._testServiceMock.Setup(x => x.GetTestByNumber(test.Id)).Returns(test);
            this._testServiceMock.Setup(x => x.HasUserAnsweredTest(It.IsAny<string>(), test.Id)).Returns(false);
            using var controller = new TestController(this._testServiceMock.Object, this._answerServiceMock.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = null }
            };

            // Act
            var result = controller.Index(test.Id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<TestDto>(viewResult.ViewData.Model);
        }

        [Fact]
        public void Index_AnswerIsNull_ReturnsViewWithError()
        {
            // Arrange
            var test = GetTestDto();
            this._testServiceMock.Setup(x => x.GetTestByNumber(It.IsAny<int>())).Returns(test);
            using var controller = new TestController(this._testServiceMock.Object, this._answerServiceMock.Object);

            // Act
            var result = controller.Index(test.Number, null);

            // Assert
            var allErrors = controller.ModelState.Values.SelectMany(v => v.Errors);
            Assert.Single(allErrors);
            Assert.Contains(allErrors, x => x.ErrorMessage == @GiveUsYourAnswer);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<TestDto>(viewResult.ViewData.Model);
            Assert.Equal("Index", viewResult.ViewName);
        }

        [Fact]
        public void Index_TestIsNull_ReturnsEmptyNotFoundResult()
        {
            // Arrange
            var test = GetTest(TestStatus.Started);
            this._testServiceMock.Setup(x => x.GetTestByNumber(test.Id)).Returns((TestDto)null);
            using var controller = new TestController(this._testServiceMock.Object, this._answerServiceMock.Object);

            // Act
            var result = controller.Index(test.Id, "answer");

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, viewResult.StatusCode);
        }

        [Fact]
        public void Index_UserHasAnswered_ReturnsViewWithTestDto()
        {
            // Arrange
            var test = GetTest(TestStatus.Ended);
            this._testServiceMock.Setup(x => x.GetTestByNumber(test.Id)).Returns(test);
            this._testServiceMock.Setup(x => x.HasUserAnsweredTest(It.IsAny<string>(), test.Id)).Returns(true);
            using var controller = new TestController(this._testServiceMock.Object, this._answerServiceMock.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = GetUser() }
            };

            // Act
            var result = controller.Index(test.Id, "answer");

            // Assert
            Assert.True(test.HasUserAnswered);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<TestDto>(viewResult.ViewData.Model);
        }

        [Fact]
        public void Index_TestNotStarted_ReturnsViewWithError()
        {
            // Arrange
            var test = GetTest(TestStatus.NotStarted);
            this._testServiceMock.Setup(x => x.GetTestByNumber(test.Id)).Returns(test);
            using var controller = new TestController(this._testServiceMock.Object, this._answerServiceMock.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = GetUser() }
            };

            // Act
            var result = controller.Index(test.Id, "answer");

            // Assert
            var allErrors = controller.ModelState.Values.SelectMany(v => v.Errors);
            Assert.Single(allErrors);
            Assert.Contains(allErrors, x => x.ErrorMessage == ExceptionsMessages.ErrorOccurs);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<TestDto>(viewResult.ViewData.Model);
        }

        [Fact]
        public void Index_TestEnded_ReturnsViewWithError()
        {
            // Arrange
            var test = GetTest(TestStatus.Ended);
            this._testServiceMock.Setup(x => x.GetTestByNumber(test.Id)).Returns(test);
            using var controller = new TestController(this._testServiceMock.Object, this._answerServiceMock.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = GetUser() }
            };

            // Act
            var result = controller.Index(test.Id, "answer");

            // Assert
            var allErrors = controller.ModelState.Values.SelectMany(v => v.Errors);
            Assert.Single(allErrors);
            Assert.Contains(allErrors, x => x.ErrorMessage == ExceptionsMessages.ErrorOccurs);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<TestDto>(viewResult.ViewData.Model);
        }

        [Fact]
        public void Index_WrongAnswer_ReturnsViewWithErrorAndUserAnswer()
        {
            // Arrange
            var usersWrongAnswer = "wrongAnswer";
            var test = GetTest(TestStatus.Started);
            this._testServiceMock.Setup(x => x.GetTestByNumber(test.Id)).Returns(test);
            this._answerServiceMock.Setup(x => x.ParseTestAnswer(usersWrongAnswer)).Returns(usersWrongAnswer);
            using var controller = new TestController(this._testServiceMock.Object, this._answerServiceMock.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = GetUser() }
            };

            // Act
            var result = controller.Index(test.Id, usersWrongAnswer);

            // Assert
            var allErrors = controller.ModelState.Values.SelectMany(v => v.Errors);
            Assert.Single(allErrors);
            Assert.Contains(allErrors, x => x.ErrorMessage == ExceptionsMessages.ErrorTryAgain);
            var viewResult = Assert.IsType<ViewResult>(result);
            var answer = Assert.IsType<TestDto>(viewResult.ViewData.Model);
            Assert.Equal(usersWrongAnswer, answer.UserAnswer, ignoreCase: true);
        }

        [Fact]
        public void Index_CorrectAnswer_ReturnsAnswerViewModel()
        {
            // Arrange
            var test = GetTest(TestStatus.Started);
            var correctAnswer = test.Answers.First().Answer;
            var user = GetUser();
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            this._testServiceMock.Setup(x => x.GetTestByNumber(test.Id)).Returns(test);
            this._testServiceMock.Setup(x => x.VerifyTestAnswer(It.IsAny<string>(), It.IsAny<List<string>>())).Returns(true);
            var userTestCorrectAnswerDto = new UserTestCorrectAnswerDto
            {
                TestId = test.Id,
                UserId = userId,
                UserFullName = null,
                AnsweringTime = default,
                AnsweringTimeOffset = default
            };
            this._testServiceMock.Setup(x => x.GetAnswerByTestId(test.Id)).Returns(userTestCorrectAnswerDto);
            using var controller = new TestController(this._testServiceMock.Object, this._answerServiceMock.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // Act
            var result = controller.Index(test.Id, correctAnswer);

            // Assert
            this._testServiceMock.Verify(x => x.AddTestAnswer(test.Id, userId, test.StartDate.Value), Times.Once);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<AnswerViewModel>(viewResult.ViewData.Model);
        }

        private static TestDto GetTest(TestStatus status)
        {
            var test = GetTestDto();
            test.Status = status;
            return test;
        }

        private static ClaimsPrincipal GetUser() =>
            new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                        new Claim(ClaimTypes.NameIdentifier, "UserId"),
                        new Claim(ClaimTypes.Name, "Name"),
                        new Claim(ClaimTypes.Role, "Role"),
            }));
    }
}
