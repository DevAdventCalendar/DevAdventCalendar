using AutoMapper;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Models;
using DevAdventCalendarCompetition.Services.Profiles;
using Moq;
using System;
using System.Collections.Generic;
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
            _adminRepositoryMock = new Mock<IAdminRepository>();
            _baseTestRepositoryMock = new Mock<IBaseTestRepository>();
        }

        [Fact]
        public void GetAllTests_ReturnTestDtoList()
        {
            //Arrange
            _adminRepositoryMock.Setup(mock => mock.GetAll()).Returns(_testList);
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var adminService = new AdminService(_adminRepositoryMock.Object, _baseTestRepositoryMock.Object, _mapper);
            //Act
            var result = adminService.GetAllTests();
            //Assert
            Assert.IsType<List<TestDto>>(result);
            Assert.True(_testList.Count == result.Count);
            _adminRepositoryMock.Verify(mock => mock.GetAll(), Times.Once())
;
        }

        [Fact]
        public void GetTestById_ReturnTestDto()
        {
            //Arrange
            _adminRepositoryMock.Setup(mock => mock.GetById(It.IsAny<int>())).Returns(_currentTest);
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var adminService = new AdminService(_adminRepositoryMock.Object, _baseTestRepositoryMock.Object, _mapper);
            //Act
            var result = adminService.GetTestById(_currentTest.Id);
            //Assert
            Assert.IsType<TestDto>(result);
            _adminRepositoryMock.Verify(mock => mock.GetById(It.Is<int>(x => x.Equals(_currentTest.Id))), Times.Once());
        }

        [Fact]
        public void GetPreviousTest_ReturnPreviousTestDto()
        {
            //Arrange
            _baseTestRepositoryMock.Setup(mock => mock.GetByNumber(It.IsAny<int>())).Returns(_previousTest);
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var adminService = new AdminService(_adminRepositoryMock.Object, _baseTestRepositoryMock.Object, _mapper);
            //Act
            var result = adminService.GetPreviousTest(_previousTest.Number);
            //Assert
            Assert.IsType<TestDto>(result);
            _baseTestRepositoryMock.Verify(mock => mock.GetByNumber(It.Is<int>(x => x.Equals(_previousTest.Number))), Times.Once());
        }

        [Fact]
        public void UpdateTestDates()
        {
            //Arrange
            _adminRepositoryMock.Setup(mock => mock.UpdateDates(It.IsAny<Test>()));
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var adminService = new AdminService(_adminRepositoryMock.Object, _baseTestRepositoryMock.Object, _mapper);
            //Act
            adminService.UpdateTestDates(_testDto, (DateTime)_testDto.StartDate, (DateTime)_testDto.EndDate);
            //Assert
            _adminRepositoryMock.Verify(mock => mock.UpdateDates(It.Is<Test>(x => x.Number == _testDto.Number && x.Id == _testDto.Id && x.StartDate == _testDto.StartDate && x.EndDate == _testDto.EndDate)), Times.Once());
        }

        [Fact]
        public void UpdateTestEndDate()
        {
            //Arrange
            _adminRepositoryMock.Setup(mock => mock.UpdateEndDate(It.IsAny<Test>()));
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>()).CreateMapper();
            var adminService = new AdminService(_adminRepositoryMock.Object, _baseTestRepositoryMock.Object, _mapper);
            //Act
            adminService.UpdateTestEndDate(_testDto, (DateTime)_testDto.EndDate);
            //Assert
            _adminRepositoryMock.Verify(mock => mock.UpdateEndDate(It.Is<Test>(x => x.Number == _testDto.Number && x.Id == _testDto.Id && x.StartDate == _testDto.StartDate && x.EndDate == _testDto.EndDate)), Times.Once());
        }
    }
}