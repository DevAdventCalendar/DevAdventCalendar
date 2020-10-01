using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DevAdventCalendarCompetition.Controllers;
using DevAdventCalendarCompetition.Models.Home;
using DevAdventCalendarCompetition.Models.Test;
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
    public class HomeControllerTests
    {
        private readonly Mock<IHomeService> _homeServiceMock;
        private readonly Mock<IAdventService> _adventService;

        public HomeControllerTests()
        {
            this._homeServiceMock = new Mock<IHomeService>();
            this._adventService = new Mock<IAdventService>();
        }

        [Fact]
        public void Index_IsNotAdventTime_ReturnsEmptyViewResult()
        {
            // Arrange
            this._adventService.Setup(x => x.IsAdvent()).Returns(false);
            using var controller = new HomeController(this._homeServiceMock.Object, this._adventService.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.Model);
        }

        [Fact]
        public void Index_UserIsNull_ReturnsEmptyViewResult()
        {
            // Arrange
            var currentTestList = new List<TestDto>()
            {
                GetTestDto()
            };
            this._adventService.Setup(x => x.IsAdvent()).Returns(true);
            using var controller = new HomeController(this._homeServiceMock.Object, this._adventService.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = null }
            };

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.Model);
        }

        [Fact]
        public void Index_CurrentTestDtoIsNull_ReturnsEmptyViewResult()
        {
            // Arrange
            this._homeServiceMock.Setup(x => x.GetCurrentTests()).Returns((List<TestDto>)null);
            this._adventService.Setup(x => x.IsAdvent()).Returns(true);
            using var controller = new HomeController(this._homeServiceMock.Object, this._adventService.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = GetUser() }
            };

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.Model);
        }

        [Fact]
        public void Index_ReturnsCorrectAnswersForUser()
        {
            // Arrange
            var user = GetUser();
            var correctAnswersCount = 3;
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentTestList = new List<TestDto>
            {
                GetTestDto()
            };
            this._homeServiceMock.Setup(x => x.GetCurrentTests()).Returns(currentTestList);
            this._homeServiceMock.Setup(x => x.GetCorrectAnswersCountForUser(userId)).Returns(correctAnswersCount);
            this._adventService.Setup(x => x.IsAdvent()).Returns(true);
            using var controller = new HomeController(this._homeServiceMock.Object, this._adventService.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var actualList = Assert.IsType<List<TestDto>>(viewResult.ViewData.Model);
            Assert.Equal(currentTestList, actualList);
        }

        [Fact]
        public void CheckTestStatus_ReturnsContentResultWithTestStatus()
        {
            // Arrange
            var test = GetTest(TestStatus.Started);
            var testStatus = test.Status.ToString();
            this._homeServiceMock.Setup(x => x.CheckTestStatus(It.IsAny<int>())).Returns(testStatus);
            using var controller = new HomeController(this._homeServiceMock.Object, this._adventService.Object);

            // Act
            var result = controller.CheckTestStatus(test.Id);

            // Assert
            this._homeServiceMock.Verify(x => x.CheckTestStatus(test.Id), Times.Once);
            var contentResult = Assert.IsType<ContentResult>(result);
            var actualStatus = Assert.IsType<string>(contentResult.Content);
            Assert.Equal(testStatus, actualStatus);
        }

        [Fact]
        public void Error_ReturnsPageNotFoundErrorViewModel()
        {
            // Arrange
            var statusCode = 404;
            var expectedMessage = ErrorMessages.NotFound;
            using var controller = new HomeController(this._homeServiceMock.Object, this._adventService.Object);

            // Act
            var result = controller.Error(statusCode);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var accualMessage = Assert.IsType<ErrorViewModel>(viewResult.ViewData.Model);
            Assert.Equal(expectedMessage, accualMessage.Message.ToString());
        }

        private static ClaimsPrincipal GetUser() =>
            new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "UserId"),
                new Claim(ClaimTypes.Name, "Name"),
                new Claim(ClaimTypes.Role, "Role"),
            }));

        private static TestDto GetTest(TestStatus status)
        {
            var test = GetTestDto();
            test.Status = status;
            return test;
        }
    }
}
