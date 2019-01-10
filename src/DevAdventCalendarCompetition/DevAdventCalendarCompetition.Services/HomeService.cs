using AutoMapper;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
            _homeRepository = homeRepository;
            _mapper = mapper;
        }

        public TestDto GetCurrentTest()
        {
            var test = _homeRepository.GetCurrentTest();
            if (test == null || (test.StartDate.HasValue && test.StartDate.Value.Date != DateTime.Today))
                return null;
            var testDto = _mapper.Map<TestDto>(test);
            return testDto;
        }

        public TestAnswerDto GetTestAnswerByUserId(string userId, int testId)
        {
            var testAnswer = _homeRepository.GetTestAnswerByUserId(userId, testId);
            var testAnswerDto = _mapper.Map<TestAnswerDto>(testAnswer);
            return testAnswerDto;
        }

        public List<TestDto> GetCurrentTests()
        {
            var testList = _homeRepository.GetAllTests();
            var allTestsDtoList = _mapper.Map<List<TestDto>>(testList);
            return allTestsDtoList;
        }

        public List<TestWithAnswerListDto> GetTestsWithUserAnswers()
        {
            var testList = _homeRepository.GetTestsWithUserAnswers();
            var testWithAnswersDtoList = _mapper.Map<List<TestWithAnswerListDto>>(testList);
            return testWithAnswersDtoList;
        }

        public List<TestResultDto> GetAllTestResults()
        {
            var testResultList = _homeRepository.GetAllTestResults();
            var testResultsDtoList = _mapper.Map<List<TestResultDto>>(testResultList);

            return testResultsDtoList;
        }

        public string CheckTestStatus(int testNumber)
        {
            var test = _homeRepository.GetTestByNumber(testNumber);

            return test == null ? TestStatus.NotStarted.ToString() : test.Status.ToString();
        }

        public int GetCorrectAnswersCountForUser(string userId)
        {
            return _homeRepository.GetCorrectAnswersCountForUser(userId);
        }

        public int GetUserPosition(string userId)
        {
            return _homeRepository.GetUserPosition(userId);
        }

        public string PrepareUserEmailForRODO(string email)
        {
            if (string.IsNullOrEmpty(email)) return string.Empty;
            var emailMaskRegex = "(^[\\w\\.\\-]{3}).*(@[\\w\\.\\-]).*(.)$";

            return Regex.Replace(email, emailMaskRegex,
                m => m.Groups[1].Value + "..." + m.Groups[2].Value + "..." + m.Groups[3].Value);
        }
    }
}