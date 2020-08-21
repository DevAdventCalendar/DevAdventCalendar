using System;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Models;
using FluentAssertions;
using Moq;
using Xunit;
using static DevAdventCalendarCompetition.Tests.TestHelper;

namespace DevAdventCalendarCompetition.Tests.UnitTests
{
    public class AdminServiceTest
    {
        [Theory]
        [AutoMoqData]
        public void GetAllTests_ReturnTestDtoList([Frozen] Mock<ITestRepository> testRepositoryMock, AdminService adminService)
        {
            // Arrange
            var testList = GetTestList();
#pragma warning disable CA1062 // Validate arguments of public methods
            testRepositoryMock.Setup(mock => mock.GetAllTests()).Returns(testList);
#pragma warning restore CA1062 // Validate arguments of public methods

            // Act
#pragma warning disable CA1062 // Validate arguments of public methods
            var result = adminService.GetAllTests();
#pragma warning restore CA1062 // Validate arguments of public methods

            // Assert
            result.Should().BeOfType<List<TestDto>>();
            result.Count.Should().Be(testList.Count);
        }

        [Theory]
        [AutoMoqData]
        public void GetTestBy_IdReturnTestDto([Frozen] Mock<ITestRepository> testRepositoryMock, AdminService adminService)
        {
            // Arrange
            var test = GetTest();
#pragma warning disable CA1062 // Validate arguments of public methods
            testRepositoryMock.Setup(mock => mock.GetTestById(It.IsAny<int>())).Returns(test);
#pragma warning restore CA1062 // Validate arguments of public methods

            // Act
#pragma warning disable CA1062 // Validate arguments of public methods
            var result = adminService.GetTestById(test.Id);
#pragma warning restore CA1062 // Validate arguments of public methods

            // Assert
            result.Should().BeOfType<TestDto>();
            result.Id.Should().Be(test.Id);
            result.Number.Should().Be(test.Number);
            result.StartDate.Should().Be(test.StartDate);
            result.EndDate.Should().Be(test.EndDate);
        }

        [Theory]
        [AutoMoqData]
        public void GetPreviousTest_ReturnPreviousTestDto([Frozen] Mock<ITestRepository> testRepositoryMock, AdminService adminService)
        {
            // Arrange
            var currentTestNumber = GetTest().Number;
            var previousTestNumber = currentTestNumber - 1;
            var previousTest = GetTest(previousTestNumber);
#pragma warning disable CA1062 // Validate arguments of public methods
            testRepositoryMock.Setup(mock => mock.GetTestByNumber(previousTestNumber)).Returns(previousTest);
#pragma warning restore CA1062 // Validate arguments of public methods

            // Act
#pragma warning disable CA1062 // Validate arguments of public methods
            var result = adminService.GetPreviousTest(currentTestNumber);
#pragma warning restore CA1062 // Validate arguments of public methods

            // Assert
            result.Should().BeOfType<TestDto>();
            result.Number.Should().Be(previousTest.Number);
            result.StartDate.Should().Be(previousTest.StartDate);
            result.EndDate.Should().Be(previousTest.EndDate);
        }

        [Theory]
        [AutoMoqData]
        public void AddTest_AddCorrectAmountOfAnswers([Frozen] Mock<ITestRepository> testRepositoryMock, AdminService adminService)
        {
            // Arrange
            var test = GetTestDto();
#pragma warning disable CA1062 // Validate arguments of public methods
            testRepositoryMock.Setup(mock => mock.AddTest(It.IsAny<Test>()));
#pragma warning restore CA1062 // Validate arguments of public methods

            // Act
#pragma warning disable CA1062 // Validate arguments of public methods
            adminService.AddTest(test);
#pragma warning restore CA1062 // Validate arguments of public methods

            // Assert
            testRepositoryMock.Verify(mock => mock.AddTest(It.Is<Test>(t => t.HashedAnswers.Count == test.Answers.Count)));
        }

        [Theory]
        [AutoMoqData]
        public void UpdateTestDates(int testId, [Frozen] Mock<ITestRepository> testRepositoryMock, AdminService adminService)
        {
            // Act
#pragma warning disable CA1062 // Validate arguments of public methods
            adminService.UpdateTestDates(testId, "20");
#pragma warning restore CA1062 // Validate arguments of public methods

            // Assert
#pragma warning disable CA1062 // Validate arguments of public methods
            testRepositoryMock.Verify(mock => mock.UpdateTestDates(testId, It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once());
#pragma warning restore CA1062 // Validate arguments of public methods
        }

        [Theory]
        [AutoMoqData]
        public void UpdateTestEndDate(int testId, DateTime newDate, [Frozen] Mock<ITestRepository> testRepositoryMock, AdminService adminService)
        {
            // Act
#pragma warning disable CA1062 // Validate arguments of public methods
            adminService.UpdateTestEndDate(testId, newDate);
#pragma warning restore CA1062 // Validate arguments of public methods

            // Assert
#pragma warning disable CA1062 // Validate arguments of public methods
            testRepositoryMock.Verify(mock => mock.UpdateTestEndDate(testId, newDate), Times.Once());
#pragma warning restore CA1062 // Validate arguments of public methods
        }
    }
}