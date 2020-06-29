using System;
using System.Collections.Generic;
using AutoMapper;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services
{
    public class AdminService : IAdminService
    {
        private readonly ITestRepository _testRepository;
        private readonly ITestAnswerRepository _testAnswerRepository;
        private readonly IMapper _mapper;
        private readonly StringHasher _stringHasher;

        public AdminService(
            ITestRepository adminRepository,
            ITestAnswerRepository testAnwserRepository,
            IMapper mapper,
            StringHasher stringHasher)
        {
            this._testRepository = adminRepository;
            this._testAnswerRepository = testAnwserRepository;
            this._mapper = mapper;
            this._stringHasher = stringHasher;
        }

        public List<TestDto> GetAllTests()
        {
            var testList = this._testRepository.GetAllTests();
            var testDtoList = this._mapper.Map<List<TestDto>>(testList);
            return testDtoList;
        }

#pragma warning disable CA1725 // Parameter names should match base declaration
        public TestDto GetTestById(int testId)
#pragma warning restore CA1725 // Parameter names should match base declaration
        {
            var test = this._testRepository.GetTestById(testId);
            var testDto = this._mapper.Map<TestDto>(test);
            return testDto;
        }

        public TestDto GetPreviousTest(int testNumber)
        {
            var test = this._testRepository.GetTestByNumber(testNumber - 1);
            var testDto = this._mapper.Map<TestDto>(test);
            return testDto;
        }

        public void AddTest(TestDto testDto)
        {
            if (testDto == null)
            {
                throw new ArgumentNullException(nameof(testDto));
            }

            string hashedAnswer = this._stringHasher.ComputeHash(testDto.Answer);

            var test = this._mapper.Map<Test>(testDto);
            test.HashedAnswer = hashedAnswer;
            this._testRepository.AddTest(test);
        }

        public void UpdateTestDates(int testId, string minutesString)
        {
            var parsed = uint.TryParse(minutesString, out uint minutes);
            if (!parsed)
            {
                minutes = 20;
            }

            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddMinutes(minutes);

            this._testRepository.UpdateTestDates(testId, startDate, endDate);
        }

        public void UpdateTestEndDate(int testId, DateTime endTime)
        {
            this._testRepository.UpdateTestEndDate(testId, endTime);
        }

        public void ResetTestDates()
        {
            this._testRepository.ResetTestDates();
        }

        public void ResetTestAnswers()
        {
            this._testAnswerRepository.DeleteTestAnswers();
        }
    }
}