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
        private readonly IMapper _mapper;
        private readonly StringHasher _stringHasher;

        public AdminService(
            IAdminRepository adminRepository,
            IBaseTestRepository baseTestRepository,
            IMapper mapper, StringHasher stringHasher)
        {
            _adminRepository = adminRepository;
            _baseTestRepository = baseTestRepository;
            _mapper = mapper;
            _stringHasher = stringHasher;
        }

        public List<TestDto> GetAllTests()
        {
            var testList = _adminRepository.GetAll();
            var testDtoList = _mapper.Map<List<TestDto>>(testList);
            return testDtoList;
        }

        public TestDto GetTestById(int testId)
        {
            var test = _adminRepository.GetById(testId);
            var testDto = _mapper.Map<TestDto>(test);
            return testDto;
        }

        public TestDto GetPreviousTest(int testNumber)
        {
            var test = _baseTestRepository.GetByNumber(testNumber);
            var testDto = _mapper.Map<TestDto>(test);
            return testDto;
        }

        public void AddTest(TestDto testDto)
        {
            string hashedAnswer = _stringHasher.ComputeHash(testDto.Answer);
            var test = _mapper.Map<Test>(testDto);
            test.HashedAnswer = hashedAnswer;
            _baseTestRepository.AddTest(test);
        }

        public void UpdateTestDates(TestDto testDto, DateTime startTime, DateTime endTime)
        {
            testDto.StartDate = startTime;
            testDto.EndDate = endTime;
            var test = _mapper.Map<Test>(testDto);
            _adminRepository.UpdateDates(test);
        }

        public void UpdateTestEndDate(TestDto testDto, DateTime endTime)
        {
            testDto.EndDate = endTime;
            var test = _mapper.Map<Test>(testDto);
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