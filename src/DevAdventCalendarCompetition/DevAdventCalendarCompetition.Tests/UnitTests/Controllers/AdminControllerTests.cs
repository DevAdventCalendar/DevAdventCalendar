using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DevAdventCalendarCompetition.Controllers;
using DevAdventCalendarCompetition.Models.Test;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Resources;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using DevAdventCalendarCompetition.Services.Options;
using Microsoft.AspNetCore.Authorization;
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
        private readonly Mock<IAnswerService> _answerServiceMock;

        public AdminControllerTests()
        {
           this._testServiceMock = new Mock<ITestService>();
           this._adminServiceMock = new Mock<IAdminService>();
           this._answerServiceMock = new Mock<IAnswerService>();
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

            using var controller = this.CreateAdminController();

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<TestDto>>(viewResult.ViewData.Model);
            Assert.Single(model);
        }

        [Fact]
        public void AddTest_ModelIsNull_ThrowsException()
        {
            // Arrange
            using var controller = this.CreateAdminController();

            // Act
            Func<ActionResult> act = () => controller.AddTest(null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void AddTest_ModelStateIsInvalid_ReturnsViewWithInvalidModel()
        {
            // Arrange
            using var controller = this.CreateAdminController();
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
            using var controller = this.CreateAdminController();

            // Act
            var result = controller.AddTest(GetTestViewModel());

            // Assert
            var allErrors = controller.ModelState.Values.SelectMany(v => v.Errors);
            Assert.Single(allErrors);
            Assert.Contains(allErrors, x => x.ErrorMessage == ExceptionsMessages.TestAlreadyExists);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<TestViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public void AddTest_TestNotExists_ReturnsARedirectAndAddsTest()
        {
            // Arrange
            this._testServiceMock.Setup(x => x.GetTestByNumber(It.IsAny<int>())).Returns((TestDto)null);
            using var controller = this.CreateAdminController();

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
            using var controller = this.CreateAdminController();

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
            using var controller = this.CreateAdminController();

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
            using var controller = this.CreateAdminController();

            // Act
            var result = controller.StartTest(test.Id, minutes);

            // Assert
            this._adminServiceMock.Verify(x => x.UpdateTestDates(test.Id, minutes), Times.Once);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void EndTest_TestIsNotStarted_ThrowsException()
        {
            // Arrange
            var test = GetTest(TestStatus.NotStarted);
            this._adminServiceMock.Setup(x => x.GetTestById(It.IsAny<int>())).Returns(test);
            using var controller = this.CreateAdminController();

            // Act
            Func<ActionResult> act = () => controller.EndTest(test.Id);

            // Assert
            var exception = Assert.Throws<InvalidOperationException>(act);
            Assert.Equal(ExceptionsMessages.TestAlreadyRun, exception.Message);
        }

        [Fact]
        public void EndTest_TestIsStarted_ReturnsRedirectToAction()
        {
            // Arrange
            var test = GetTest(TestStatus.Started);
            this._adminServiceMock.Setup(x => x.GetTestById(It.IsAny<int>())).Returns(test);
            using var controller = this.CreateAdminController();

            // Act
            var result = controller.EndTest(test.Id);

            // Assert
            this._adminServiceMock.Verify(x => x.UpdateTestEndDate(test.Id, It.IsAny<DateTime>()), Times.Once);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void AdminController_ShouldHaveAuthorizeAttributeWithCorrectRole()
        {
            var attribute = GetAuthorizeAttributeFromController();

            Assert.NotNull(attribute);
            Assert.NotNull(attribute.Roles);
            Assert.Equal("Admin", attribute.Roles);
        }

        [Fact]
        public void AdminController_ShouldNotHaveAllowAnonymousAttribute()
        {
            var actions = GetActionsWithAllowAnonymousAttribute();
            var allowAnonymousAttr = GetAllowAnonymousAttributeFromController();

            Assert.Empty(actions);
            Assert.Null(allowAnonymousAttr);
        }

        private static AllowAnonymousAttribute GetAllowAnonymousAttributeFromController() => typeof(AdminController)
                .GetCustomAttributes(typeof(AllowAnonymousAttribute), true)
                .FirstOrDefault() as AllowAnonymousAttribute;

        private static AuthorizeAttribute GetAuthorizeAttributeFromController() => typeof(AdminController)
            .GetCustomAttributes(typeof(AuthorizeAttribute), true)
            .FirstOrDefault() as AuthorizeAttribute;

        private static IEnumerable<MethodInfo> GetActionsWithAllowAnonymousAttribute() => typeof(AdminController)
            .GetMethods()
            .Where(m => m.IsDefined(typeof(AllowAnonymousAttribute)));

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
            PartnerName = null,
            PartnerLogoUrl = null,
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

        private AdminController CreateAdminController()
        {
            return new AdminController(this._adminServiceMock.Object, this._answerServiceMock.Object, this._testServiceMock.Object, new TestSettings());
        }
    }
}
