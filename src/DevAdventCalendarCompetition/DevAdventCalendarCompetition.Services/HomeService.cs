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
        private readonly IHomeRepository _homeRepository;
        private readonly IMapper _mapper;

        public HomeService(
            IHomeRepository homeRepository,
            IMapper mapper)
        {
            this._homeRepository = homeRepository;
            this._mapper = mapper;
        }

        public TestDto GetCurrentTest()
        {
            var test = this._homeRepository.GetCurrentTest();
            if (test == null || (test.StartDate.HasValue && test.StartDate.Value.Date != DateTime.Today))
            {
                return null;
            }

            var testDto = this._mapper.Map<TestDto>(test);
            return testDto;
        }

        public TestAnswerDto GetTestAnswerByUserId(string userId, int testId)
        {
            var testAnswer = this._homeRepository.GetTestAnswerByUserId(userId, testId);
            var testAnswerDto = this._mapper.Map<TestAnswerDto>(testAnswer);
            return testAnswerDto;
        }

        public List<TestDto> GetCurrentTests()
        {
            var testList = this._homeRepository.GetAllTests();
            var allTestsDtoList = this._mapper.Map<List<TestDto>>(testList);
            return allTestsDtoList;
        }

        public List<TestWithAnswerListDto> GetTestsWithUserAnswers()
        {
            var testList = this._homeRepository.GetTestsWithUserAnswers();
            var testWithAnswersDtoList = this._mapper.Map<List<TestWithAnswerListDto>>(testList);
            return testWithAnswersDtoList;
        }

        public Dictionary<int, List<TestResultDto>> GetAllTestResults()
        {
            var testResultDictionary = new Dictionary<int, List<TestResultDto>>();

            /*
             * TODO: Get all results from repo for every weeks' dates range
             */

            var testResultList = this._homeRepository.GetAllTestResults();
            var testResultsDtoList = this._mapper.Map<List<TestResultDto>>(testResultList);

            return testResultDictionary;
        }

        public string CheckTestStatus(int testNumber)
        {
            var test = this._homeRepository.GetTestByNumber(testNumber);

            return test == null ? TestStatus.NotStarted.ToString() : test.Status.ToString();
        }

        public int GetCorrectAnswersCountForUser(string userId)
        {
            return this._homeRepository.GetCorrectAnswersCountForUser(userId);
        }

        public int GetUserPosition(string userId)
        {
            return this._homeRepository.GetUserPosition(userId);
        }

        public string PrepareUserEmailForRODO(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return string.Empty;
            }

            var emailMaskRegex = "(^([\\w\\.\\-]{3})|(\\w{1,2})).*(@[\\w\\.\\-]).*(.)$";

            return Regex.Replace(email, emailMaskRegex,
                m => string.IsNullOrEmpty(m.Groups[3].Value)
                    ? m.Groups[2].Value + "..." + m.Groups[4].Value + "..." + m.Groups[5].Value
                    : m.Groups[3].Value + "..." + m.Groups[4].Value + "..." + m.Groups[5].Value);
        }
    }
}