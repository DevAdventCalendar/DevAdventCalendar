using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DevAdventCalendarCompetition.Controllers;
using DevAdventCalendarCompetition.Models.Test;
using DevAdventCalendarCompetition.Repository.Models;
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

        public HomeControllerTests()
        {
            this._homeServiceMock = new Mock<IHomeService>();
        }

        [Fact]
        public void Index_CurrentTestDtoIsNull_ReturnsEmptyViewResult()
        {
            // Arrange
            this._homeServiceMock.Setup(x => x.GetCurrentTests()).Returns((List<TestDto>)null);
            using var controller = new HomeController(this._homeServiceMock.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.Model);
        }

        [Fact]
        public void Index_UserIsNull_ReturnsAViewResultWithListOfTestDto()
        {
            // Arrange
            var currentTestList = new List<TestDto>()
            {
                GetTestDto()
            };
            this._homeServiceMock.Setup(x => x.GetCurrentTests()).Returns(currentTestList);
            using var controller = new HomeController(this._homeServiceMock.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = null }
            };

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<List<TestDto>>(viewResult.ViewData.Model);
        }

        [Fact]
        public void Index_ReturnsCorrectAnswersForUser()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var user = GetUser();
            var correctAnswersCount = 3;
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            httpContext.HttpContext.User = user;

            var currentTestList = new List<TestDto>
            {
                GetTestDto()
            };

            this._homeServiceMock.Setup(x => x.GetCurrentTests()).Returns(currentTestList);
            this._homeServiceMock.Setup(x => x.GetCorrectAnswersCountForUser(userId)).Returns(correctAnswersCount);
            using var controller = new HomeController(this._homeServiceMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
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
            using var controller = new HomeController(this._homeServiceMock.Object);

            // Act
            var result = controller.CheckTestStatus(test.Id);

            // Assert
            this._homeServiceMock.Verify(x => x.CheckTestStatus(test.Id), Times.Once);
            var contentResult = Assert.IsType<ContentResult>(result);
            var actualStatus = Assert.IsType<string>(contentResult.Content.ToString());
            Assert.Equal(testStatus, actualStatus);
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
