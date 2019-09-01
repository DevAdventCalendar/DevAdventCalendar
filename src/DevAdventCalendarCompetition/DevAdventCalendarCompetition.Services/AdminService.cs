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
        private readonly IAdminRepository _adminRepository;
        private readonly IBaseTestRepository _baseTestRepository;
        private readonly IMapper _mapper;
        private readonly StringHasher _stringHasher;

        public AdminService(
            IAdminRepository adminRepository,
            IBaseTestRepository baseTestRepository,
            IMapper mapper,
            StringHasher stringHasher)
        {
            this._adminRepository = adminRepository;
            this._baseTestRepository = baseTestRepository;
            this._mapper = mapper;
            this._stringHasher = stringHasher;
        }

        public List<TestDto> GetAllTests()
        {
            var testList = this._adminRepository.GetAll();
            var testDtoList = this._mapper.Map<List<TestDto>>(testList);
            return testDtoList;
        }

#pragma warning disable CA1725 // Parameter names should match base declaration
        public TestDto GetTestById(int testId)
#pragma warning restore CA1725 // Parameter names should match base declaration
        {
            var test = this._adminRepository.GetById(testId);
            var testDto = this._mapper.Map<TestDto>(test);
            return testDto;
        }

        public TestDto GetPreviousTest(int testNumber)
        {
            var test = this._baseTestRepository.GetByNumber(testNumber - 1);
            var testDto = this._mapper.Map<TestDto>(test);
            return testDto;
        }

        public void AddTest(TestDto testDto)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            string hashedAnswer = this._stringHasher.ComputeHash(testDto.Answer);
#pragma warning restore CA1062 // Validate arguments of public methods
            var test = this._mapper.Map<Test>(testDto);
            test.HashedAnswer = hashedAnswer;
            this._baseTestRepository.AddTest(test);
        }

#pragma warning disable CA1725 // Parameter names should match base declaration
        public void UpdateTestDates(TestDto testDto, string minutesString)
#pragma warning restore CA1725 // Parameter names should match base declaration
        {
            uint minutes = 0;
            var parsed = uint.TryParse(minutesString, out minutes);
            if (!parsed)
            {
                minutes = 20;
            }

#pragma warning disable CA1062 // Validate arguments of public methods
            testDto.StartDate = DateTime.Now;
#pragma warning restore CA1062 // Validate arguments of public methods
            testDto.EndDate = DateTime.Now.AddMinutes(minutes);
            var test = this._mapper.Map<Test>(testDto);
            this._adminRepository.UpdateDates(test);
        }

#pragma warning disable CA1725 // Parameter names should match base declaration
        public void UpdateTestEndDate(TestDto testDto, DateTime endTime)
#pragma warning restore CA1725 // Parameter names should match base declaration
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            testDto.EndDate = endTime;
#pragma warning restore CA1062 // Validate arguments of public methods
            var test = this._mapper.Map<Test>(testDto);
            this._adminRepository.UpdateEndDate(test);
        }

        public void ResetTestDates()
        {
            this._adminRepository.ResetTestDates();
        }

        public void ResetTestAnswers()
        {
            this._adminRepository.DeleteTestAnswers();
        }
    }
}