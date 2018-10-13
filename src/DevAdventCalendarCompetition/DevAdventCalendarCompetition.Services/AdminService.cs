using AutoMapper;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using System;
using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IBaseTestRepository _baseTestRepository;

        public AdminService(IAdminRepository adminRepository, IBaseTestRepository baseTestRepository)
        {
            _adminRepository = adminRepository;
            _baseTestRepository = baseTestRepository;
        }

        public List<TestDto> GetAllTests()
        {
            var testList = _adminRepository.GetAll();
            var testDtoList = Mapper.Map<List<TestDto>>(testList);
            return testDtoList;
        }

        public TestDto GetTestById(int testId)
        {
            var test = _adminRepository.GetById(testId);
            var testDto = Mapper.Map<TestDto>(test);
            return testDto;
        }

        public TestDto GetPreviousTest(int testNumber)
        {
            var test = _baseTestRepository.GetByNumber(testNumber);
            var testDto = Mapper.Map<TestDto>(test);
            return testDto;
        }

        public void UpdateTestDates(TestDto testDto, DateTime startTime, DateTime endTime)
        {
            testDto.StartDate = startTime;
            testDto.EndDate = endTime;
            var test = Mapper.Map<Test>(testDto);
            _adminRepository.UpdateDates(test);
        }

        public void UpdateTestEndDate(TestDto testDto, DateTime endTime)
        {
            testDto.EndDate = endTime;
            var test = Mapper.Map<Test>(testDto);
            _adminRepository.UpdateEndDate(test);
        }

        public void ResetTestDates()
        {
            _adminRepository.ResetTestDates();
        }

        public void ResetTestAnswers()
        {
            _adminRepository.DeleteTestAnswers();
        }
    }
}