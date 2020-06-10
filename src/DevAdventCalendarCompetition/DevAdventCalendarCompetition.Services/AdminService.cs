using System;
using System.Collections.Generic;
using AutoMapper;
using DevAdventCalendarCompetition.Repository;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services
{
    public class AdminService : IAdminService
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;
        private readonly StringHasher _stringHasher;
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(
            ITestRepository adminRepository,
            IMapper mapper,
            StringHasher stringHasher,
            IUnitOfWork unitOfWork)
        {
            this._testRepository = adminRepository;
            this._mapper = mapper;
            this._stringHasher = stringHasher;
            this._unitOfWork = unitOfWork;
        }

        public List<TestDto> GetAllTests()
        {
            var tests = this._testRepository.GetAll();
            return this._mapper.Map<List<TestDto>>(tests);
        }

        public TestDto GetTestById(int id)
        {
            var test = this._testRepository.GetById(id);
            return this._mapper.Map<TestDto>(test);
        }

        public TestDto GetPreviousTest(int testNumber)
        {
            var test = this._testRepository.GetByNumber(testNumber - 1);
            return this._mapper.Map<TestDto>(test);
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
            this._unitOfWork.Commit();
        }

        public void UpdateTestDates(int testId, string minutesString)
        {
            var test = this._testRepository.GetById(testId);

            uint minutes = 0;
            var parsed = uint.TryParse(minutesString, out minutes);
            if (!parsed)
            {
                minutes = 20;
            }

            test.StartDate = DateTime.Now;
            test.EndDate = DateTime.Now.AddMinutes(minutes);
            this._unitOfWork.Commit();
        }

        public void UpdateTestEndDate(int testId, DateTime endTime)
        {
            var test = this._testRepository.GetById(testId);

            test.EndDate = endTime;
            this._unitOfWork.Commit();
        }

        public void ResetTestDates()
        {
            var tests = this._testRepository.GetAll();
            tests.ForEach(el =>
            {
                el.StartDate = null;
                el.EndDate = null;
            });

            this._unitOfWork.Commit();
        }
    }
}