using System;
using System.Collections.Generic;
using System.Security.Claims;
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
        public void Index_UserIsNull_ReturnsAViewResutWithListOfTestDto()
        {
            // Arrange
            var currentTestList = new List<TestDto>()
            {
                GetTestDto()
            };
            var user = GetClaimsUser();
            this._homeServiceMock.Setup(x => x.GetCurrentTests()).Returns(currentTestList);
            using var controller = new HomeController(this._homeServiceMock.Object);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<List<TestDto>>(viewResult.ViewData.Model);
        }

        [Fact]
        public void CheckTestStatus_ReturnsContetResultWithTestStatus()
        {
            // Arrange
            var test = GetTest(TestStatus.Started);
            this._homeServiceMock.Setup(x => x.CheckTestStatus(It.IsAny<int>())).Returns(It.IsAny<string>());
            using var controller = new HomeController(this._homeServiceMock.Object);

            // Act
            var result = controller.CheckTestStatus(test.Id);

            // Assert
            var viewResult = Assert.IsType<ContentResult>(result);
        }

        private static ClaimsPrincipal GetClaimsUser()
        {
            return new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
            {
                new Claim(ClaimTypes.Name, "example name"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim("custom-claim", "example claim value"),
            }, "mock"));
        }

        private static TestViewModel GetTestViewModel() => new TestViewModel
        {
            Number = 0,
            Description = "Description",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Answers = new List<string>()
            {
                "Answer"
            },
            SponsorName = null,
            SponsorLogoUrl = null,
            Discount = null,
            DiscountUrl = null,
            DiscountLogoUrl = null,
            DiscountLogoPath = null
        };

        private static TestDto GetTest(TestStatus status)
        {
            var test = GetTestDto();
            test.Status = status;
            return test;
        }
    }
}
