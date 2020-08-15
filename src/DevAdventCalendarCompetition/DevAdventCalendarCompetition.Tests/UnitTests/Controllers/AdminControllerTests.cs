using System;
using System.Collections.Generic;
using DevAdventCalendarCompetition.Controllers;
using DevAdventCalendarCompetition.Models.Test;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Resources;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using static DevAdventCalendarCompetition.Tests.TestHelper;

namespace DevAdventCalendarCompetition.Tests.UnitTests.Controllers
{
    public class AdminControllerTests
    {
        private readonly Mock<ITestService> _testServiceMock;
        private readonly Mock<IAdminService> _adminServiceMock;

        public AdminControllerTests()
        {
           this._testServiceMock = new Mock<ITestService>();
           this._adminServiceMock = new Mock<IAdminService>();
        }

        [Fact]
        public void Index_ReturnsAViewResultWithAListOfTestDto()
        {
            // Arrange
            var testList = new List<TestDto>()
            {
                GetTestDto()
            };

            this._adminServiceMock.Setup(x => x.GetAllTests()).Returns(testList);
#pragma warning disable CA2000 // Dispose objects before losing scope
            var controller = new AdminController(this._adminServiceMock.Object, this._testServiceMock.Object);
#pragma warning restore CA2000 // Dispose objects before losing scope

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<TestDto>>(
                viewResult.ViewData.Model);
            Assert.Single(model);
        }

        [Fact]
        public void AddTest_ModelStateIsInvalid_ReturnsViewWithInvalidModel()
        {
            // Arrange
#pragma warning disable CA2000 // Dispose objects before losing scope
            var controller = new AdminController(this._adminServiceMock.Object, this._testServiceMock.Object);
#pragma warning restore CA2000 // Dispose objects before losing scope
            controller.ModelState.AddModelError("Number", "Required");

            // Act
            var result = controller.AddTest(GetTestViewModel());

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<TestViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public void AddTest_TestExists_ReturnsViewWithError()
        {
            // Arrange
            this._testServiceMock.Setup(x => x.GetTestByNumber(It.IsAny<int>())).Returns(GetTestDto());
#pragma warning disable CA2000 // Dispose objects before losing scope
            var controller = new AdminController(this._adminServiceMock.Object, this._testServiceMock.Object);
#pragma warning restore CA2000 // Dispose objects before losing scope

            // Act
            var result = controller.AddTest(GetTestViewModel());

            // Assert
            controller.ModelState.ErrorCount.Should().Be(1);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<TestViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public void AddTest_TestNotExists_ReturnsARedirectAndAddsTest()
        {
            // Arrange
            this._testServiceMock.Setup(x => x.GetTestByNumber(It.IsAny<int>())).Returns((TestDto)null);
#pragma warning disable CA2000 // Dispose objects before losing scope
            var controller = new AdminController(this._adminServiceMock.Object, this._testServiceMock.Object);
#pragma warning restore CA2000 // Dispose objects before losing scope

            // Act
            var result = controller.AddTest(GetTestViewModel());

            // Assert
            this._adminServiceMock.Verify(x => x.AddTest(It.IsAny<TestDto>()), Times.Once);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void StartTest_TestIsAlreadyStarted_ThrowsException()
        {
            // Arrange
            var test = GetTest(TestStatus.Started);
            this._adminServiceMock.Setup(x => x.GetTestById(test.Id)).Returns(test);
#pragma warning disable CA2000 // Dispose objects before losing scope
            var controller = new AdminController(this._adminServiceMock.Object, this._testServiceMock.Object);
#pragma warning restore CA2000 // Dispose objects before losing scope

            // Act
            Func<ActionResult> act = () => controller.StartTest(test.Id, "20");

            // Assert
            var exception = Assert.Throws<InvalidOperationException>(act);
            Assert.Equal(exception.Message, ExceptionsMessages.TestAlreadyRun);
        }

        [Fact]
        public void StartTest_PreviousTestIsNotDone_ThrowsException()
        {
            // Arrange
            var test = GetTest(TestStatus.NotStarted);
            var previousTest = GetTest(TestStatus.Started);
            this._adminServiceMock.Setup(x => x.GetTestById(It.IsAny<int>())).Returns(test);
            this._adminServiceMock.Setup(x => x.GetPreviousTest(It.IsAny<int>())).Returns(previousTest);
#pragma warning disable CA2000 // Dispose objects before losing scope
            var controller = new AdminController(this._adminServiceMock.Object, this._testServiceMock.Object);
#pragma warning restore CA2000 // Dispose objects before losing scope

            // Act
            Func<ActionResult> act = () => controller.StartTest(test.Id, "20");

            // Assert
            var exception = Assert.Throws<InvalidOperationException>(act);
            Assert.Equal(exception.Message, ExceptionsMessages.PreviousTestIsNotDone);
        }

        [Fact]
        public void StartTest_TestIsNotStartedAndPreviousOneIsNotDone_ReturnsARedirectAndUpdateTestDates()
        {
            // Arrange
            var minutes = "20";
            var test = GetTest(TestStatus.NotStarted);
            var previousTest = GetTest(TestStatus.Ended);
            this._adminServiceMock.Setup(x => x.GetTestById(It.IsAny<int>())).Returns(test);
            this._adminServiceMock.Setup(x => x.GetPreviousTest(It.IsAny<int>())).Returns(previousTest);
#pragma warning disable CA2000 // Dispose objects before losing scope
            var controller = new AdminController(this._adminServiceMock.Object, this._testServiceMock.Object);
#pragma warning restore CA2000 // Dispose objects before losing scope

            // Act
            var result = controller.StartTest(test.Id, minutes);

            // Assert
            this._adminServiceMock.Verify(x => x.UpdateTestDates(test.Id, minutes), Times.Once);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void CalculateResults_CorrectWeekNumber_ReturnsOkResult()
        {
            // Arrange
            var correctWeekNumber = 3;
#pragma warning disable CA2000 // Dispose objects before losing scope
            var controller = new AdminController(this._adminServiceMock.Object, this._testServiceMock.Object);
#pragma warning restore CA2000 // Dispose objects before losing scope

            // Act
            var result = controller.CalculateResults(correctWeekNumber);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void CalculateResults_IncorrectWeekNumber_ReturnsOkResult()
        {
            // Arrange
            var incorrectWeekNumber = -1;
#pragma warning disable CA2000 // Dispose objects before losing scope
            var controller = new AdminController(this._adminServiceMock.Object, this._testServiceMock.Object);
#pragma warning restore CA2000 // Dispose objects before losing scope

            // Act
            var result = controller.CalculateResults(incorrectWeekNumber);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
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
