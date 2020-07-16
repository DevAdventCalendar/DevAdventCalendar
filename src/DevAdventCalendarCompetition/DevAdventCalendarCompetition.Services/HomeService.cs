using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AutoMapper;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services
{
    public class HomeService : IHomeService
    {
        private readonly IUserTestAnswersRepository _testAnswerRepository;
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;

        public HomeService(
            IUserTestAnswersRepository testAnswerRepository,
            ITestRepository testRepository,
            IMapper mapper)
        {
            this._testAnswerRepository = testAnswerRepository;
            this._testRepository = testRepository;
            this._mapper = mapper;
        }

        public UserTestCorrectAnswerDto GetCorrectAnswerByUserId(string userId, int testId)
        {
            var testAnswer = this._testAnswerRepository.GetCorrectAnswerByUserId(userId, testId);
            var testAnswerDto = this._mapper.Map<UserTestCorrectAnswerDto>(testAnswer);
            return testAnswerDto;
        }

        public List<TestDto> GetCurrentTests()
        {
            var testList = this._testRepository.GetAllTests();
            var allTestsDtoList = this._mapper.Map<List<TestDto>>(testList);
            return allTestsDtoList;
        }

        public List<TestWithUserCorrectAnswerListDto> GetTestsWithUserAnswers()
        {
            var testList = this._testRepository.GetTestsWithUserAnswers();
            var testWithAnswersDtoList = this._mapper.Map<List<TestWithUserCorrectAnswerListDto>>(testList);
            return testWithAnswersDtoList;
        }

        public string CheckTestStatus(int testNumber)
        {
            var test = this._testRepository.GetTestByNumber(testNumber);

            return test == null ? TestStatus.NotStarted.ToString() : test.Status.ToString();
        }

        public int GetCorrectAnswersCountForUser(string userId)
        {
            return this._testAnswerRepository.GetCorrectAnswersCountForUser(userId);
        }
    }
}