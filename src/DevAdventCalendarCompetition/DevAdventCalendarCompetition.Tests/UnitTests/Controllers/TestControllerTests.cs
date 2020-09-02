using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using DevAdventCalendarCompetition.Controllers;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Resources;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using static DevAdventCalendarCompetition.Tests.TestHelper;

namespace DevAdventCalendarCompetition.Tests.UnitTests.Controllers
{
    public class TestControllerTests
    {
        private readonly Mock<ITestService> _testServiceMock;

        public TestControllerTests()
        {
            this._testServiceMock = new Mock<ITestService>();
        }

        [Fact]
        public void Index_UserHasAnsweredTrue_ReturnsTestDto()
        {
            // Arrange
            var test = GetTest(TestStatus.Started);
            this._testServiceMock.Setup(x => x.GetTestByNumber(test.Id)).Returns(test);
            this._testServiceMock.Setup(x => x.HasUserAnsweredTest(It.IsAny<string>(), test.Id)).Returns(true);
            using var controller = new TestController(this._testServiceMock.Object);
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
        public void Index_AnswerIsNull_ThrowsException()
        {
            // Arrange
            var test = GetTest(TestStatus.Started);
            using var controller = new TestController(this._testServiceMock.Object);

            // Act
            Func<ActionResult> act = () => controller.Index(test.Id, null);

            // Assert
            var exception = Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void Index_TestIsNull_ReturnsEmptyNotFoundResult()
        {
            // Arrange
            var test = GetTest(TestStatus.Started);
            var answer = "Answer";
            this._testServiceMock.Setup(x => x.GetTestByNumber(test.Id)).Returns((TestDto)null);
            using var controller = new TestController(this._testServiceMock.Object);

            // Act
            var result = controller.Index(test.Id, answer);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Index_TestNotStarted_ReturnsViewWithError()
        {
            // Arrange
            var test = GetTest(TestStatus.NotStarted);
            var answer = "Answer";
            var user = GetUser();
            this._testServiceMock.Setup(x => x.GetTestByNumber(test.Id)).Returns(test);
            using var controller = new TestController(this._testServiceMock.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // Act
            var result = controller.Index(test.Id, answer);

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
            var answer = "Answer";
            var user = GetUser();
            this._testServiceMock.Setup(x => x.GetTestByNumber(test.Id)).Returns(test);
            using var controller = new TestController(this._testServiceMock.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // Act
            var result = controller.Index(test.Id, answer);

            // Assert
            var allErrors = controller.ModelState.Values.SelectMany(v => v.Errors);
            Assert.Single(allErrors);
            Assert.Contains(allErrors, x => x.ErrorMessage == ExceptionsMessages.ErrorOccurs);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<TestDto>(viewResult.ViewData.Model);
        }

        [Fact]
        public void Index_WrongAnswer_ReturnsViewWithError()
        {
            // Arrange
            var test = GetTest(TestStatus.Started);
            var answer = "WrongAnswer";
            var user = GetUser();
            this._testServiceMock.Setup(x => x.GetTestByNumber(test.Id)).Returns(test);
            using var controller = new TestController(this._testServiceMock.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // Act
            var result = controller.Index(test.Id, answer);

            // Assert
            var allErrors = controller.ModelState.Values.SelectMany(v => v.Errors);
            Assert.Single(allErrors);
            Assert.Contains(allErrors, x => x.ErrorMessage == ExceptionsMessages.ErrorTryAgain);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<TestDto>(viewResult.ViewData.Model);
        }

        [Fact]
        public void Index_CorrectAnswer_ReturnsAnswerViewModel()
        {
            // Arrange
            var test = GetTest(TestStatus.Started);
            var answer = "Answer1";
            var user = GetUser();
            this._testServiceMock.Setup(x => x.GetTestByNumber(test.Id)).Returns(test);
            this._testServiceMock.Setup(x => x.VerifyTestAnswer(It.IsAny<string>(), It.IsAny<List<string>>())).Returns(true);
            using var controller = new TestController(this._testServiceMock.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // Act
            var result = controller.Index(test.Id, answer);
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
