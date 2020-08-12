using System;
using System.Collections.Generic;
using System.Resources;
using System.Text.RegularExpressions;
using AutoMapper;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Extensions;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using DevExeptionsMessages;
using Microsoft.Extensions.Configuration;

namespace DevAdventCalendarCompetition.Services
{
    public class HomeService : IHomeService
    {
        private readonly IUserTestAnswersRepository _testAnswerRepository;
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public HomeService(
            IUserTestAnswersRepository testAnswerRepository,
            ITestRepository testRepository,
            IMapper mapper,
            IConfiguration configuration)
        {
            this._testAnswerRepository = testAnswerRepository;
            this._testRepository = testRepository;
            this._mapper = mapper;
            this._configuration = configuration;
        }

        // sprawdzić czy isadvent true jesli nie to rzucić wyjatkiem
        public UserTestCorrectAnswerDto GetCorrectAnswerByUserId(string userId, int testId)
        {
            if (IsAdventExtensions.CheckIsAdvent(this._configuration) != true)
            {
                throw new InvalidOperationException(ExceptionsMessagesServices.IsNotAdvent);
            }

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