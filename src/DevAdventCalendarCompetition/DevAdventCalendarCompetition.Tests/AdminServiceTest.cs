using System;
using System.Collections.Generic;
using AutoMapper;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Models;
using DevAdventCalendarCompetition.Services.Profiles;
using Moq;
using Xunit;

namespace DevAdventCalendarCompetition.Tests
{
    public class AdminServiceTest
    {
        private readonly Mock<IAdminRepository> _adminRepositoryMock;
        private readonly Mock<IBaseTestRepository> _baseTestRepositoryMock;
        private IMapper _mapper;

        private List<Test> _testList = new List<Test>()
        {
            new Test()
            {
                Id = 1,
                Number = 1,
                StartDate = DateTime.Today.AddHours(12),
                EndDate = DateTime.Today.AddHours(23).AddMinutes(59),
                Answers = null
            }
        };

        private Test _currentTest = new Test()
        {
            Id = 2,
            Number = 2,
            StartDate = DateTime.Today.AddHours(12),
            EndDate = DateTime.Today.AddHours(23).AddMinutes(59),
            Answers = null
        };

        private Test _previousTest = new Test()
        {
            Id = 1,
            Number = 1,
            StartDate = DateTime.Today.AddDays(-2).AddHours(12),
            EndDate = DateTime.Today.AddDays(-2).AddHours(23).AddMinutes(59),
            Answers = null
        };

        private TestDto _testDto = new TestDto()
        {
            Id = 2,
            Number = 2,
            StartDate = DateTime.Today.AddHours(12),
            EndDate = DateTime.Today.AddHours(23).AddMinutes(59),
            Status = Services.Models.TestStatus.Started
        };

        public AdminServiceTest()
        {
            this._adminRepositoryMock = new Mock<IAdminRepository>();
            this._baseTestRepositoryMock = new Mock<IBaseTestRepository>();
        }

        [Fact]
        public void GetAllTests_ReturnTestDtoList()
        {
            // Arrange
            this._adminRepositoryMock.Setup(mock => mock.GetAll()).Returns(this._testList);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var adminService = new AdminService(this._adminRepositoryMock.Object, this._baseTestRepositoryMock.Object, this._mapper, null);

            // Act
            var result = adminService.GetAllTests();

            // Assert
            Assert.IsType<List<TestDto>>(result);
            Assert.True(this._testList.Count == result.Count);
            this._adminRepositoryMock.Verify(mock => mock.GetAll(), Times.Once())
;
        }

        [Fact]
        public void GetTestBy_IdReturnTestDto()
        {
            // Arrange
            this._adminRepositoryMock.Setup(mock => mock.GetById(It.IsAny<int>())).Returns(this._currentTest);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var adminService = new AdminService(this._adminRepositoryMock.Object, this._baseTestRepositoryMock.Object, this._mapper, null);

            // Act
            var result = adminService.GetTestById(this._currentTest.Id);

            // Assert
            Assert.IsType<TestDto>(result);
            this._adminRepositoryMock.Verify(mock => mock.GetById(It.Is<int>(x => x.Equals(this._currentTest.Id))), Times.Once());
        }

        [Fact]
        public void GetPreviousTest_ReturnPreviousTestDto()
        {
            // Arrange
            this._baseTestRepositoryMock.Setup(mock => mock.GetByNumber(It.IsAny<int>())).Returns(this._previousTest);
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var adminService = new AdminService(this._adminRepositoryMock.Object, this._baseTestRepositoryMock.Object, this._mapper, null);

            // Act
            var result = adminService.GetPreviousTest(this._currentTest.Number);

            // Assert
            Assert.IsType<TestDto>(result);
            this._baseTestRepositoryMock.Verify(mock => mock.GetByNumber(It.Is<int>(x => x.Equals(this._previousTest.Number))), Times.Once());
        }

        [Fact]
        public void UpdateTestDates()
        {
            // Arrange
            this._adminRepositoryMock.Setup(mock => mock.UpdateDates(It.IsAny<Test>()));
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var adminService = new AdminService(this._adminRepositoryMock.Object, this._baseTestRepositoryMock.Object, this._mapper, null);

            // Act
            adminService.UpdateTestDates(this._testDto, "20");

            // Assert
            this._adminRepositoryMock.Verify(mock => mock.UpdateDates(It.Is<Test>(x => x.Number == this._testDto.Number && x.Id == this._testDto.Id && x.StartDate == this._testDto.StartDate && x.EndDate == this._testDto.EndDate)), Times.Once());
        }

        [Fact]
        public void UpdateTestEndDate()
        {
            // Arrange
            this._adminRepositoryMock.Setup(mock => mock.UpdateEndDate(It.IsAny<Test>()));
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var adminService = new AdminService(this._adminRepositoryMock.Object, this._baseTestRepositoryMock.Object, this._mapper, null);

            // Act
            adminService.UpdateTestEndDate(this._testDto, (DateTime)this._testDto.EndDate);

            // Assert
            this._adminRepositoryMock.Verify(mock => mock.UpdateEndDate(It.Is<Test>(x => x.Number == this._testDto.Number && x.Id == this._testDto.Id && x.StartDate == this._testDto.StartDate && x.EndDate == this._testDto.EndDate)), Times.Once());
        }
    }
}