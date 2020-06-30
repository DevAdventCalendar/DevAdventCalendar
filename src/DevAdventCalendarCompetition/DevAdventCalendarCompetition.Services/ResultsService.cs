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
    public class ResultsService : IResultsService
    {
        private readonly IResultsRepository _resultsRepository;
        private readonly ITestAnswerRepository _testAnswerRepository;
        private readonly IMapper _mapper;

        public ResultsService(
            IResultsRepository resultsRepository,
            ITestAnswerRepository testAnswerRepository,
            IMapper mapper)
        {
            this._resultsRepository = resultsRepository;
            this._testAnswerRepository = testAnswerRepository;
            this._mapper = mapper;
        }

        public Dictionary<int, List<TestResultDto>> GetAllTestResults()
        {
            var testResultDictionary = new Dictionary<int, List<TestResultDto>>();
            var week1Results = this._resultsRepository.GetTestResultsForWeek(1);

            if (week1Results != null && week1Results.Count > 0)
            {
                testResultDictionary.Add(1, this.FillResultsWithAnswersStats(1, this._mapper.Map<List<TestResultDto>>(week1Results)));

                var week2Results = this._resultsRepository.GetTestResultsForWeek(2);

                if (week2Results != null && week2Results.Count > 0)
                {
                    testResultDictionary.Add(2, this.FillResultsWithAnswersStats(2, this._mapper.Map<List<TestResultDto>>(week2Results)));

                    var week3Results = this._resultsRepository.GetTestResultsForWeek(3);

                    if (week3Results != null && week3Results.Count > 0)
                    {
                        testResultDictionary.Add(3, this.FillResultsWithAnswersStats(3, this._mapper.Map<List<TestResultDto>>(week3Results)));

                        var fullResults = this._resultsRepository.GetTestResultsForWeek(4);

                        if (fullResults != null && fullResults.Count > 0)
                        {
                            testResultDictionary.Add(4, this.FillResultsWithAnswersStats(4, this._mapper.Map<List<TestResultDto>>(fullResults)));
                        }
                    }
                }
            }

            return testResultDictionary;
        }

        public UserPosition GetUserPosition(string userId)
        {
            return this._resultsRepository.GetUserPosition(userId);
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

        private List<TestResultDto> FillResultsWithAnswersStats(int weekNumber, List<TestResultDto> results)
        {
            DateTimeOffset dateFrom;
            DateTimeOffset dateTo;

            switch (weekNumber)
            {
                case 1:
                    dateFrom = new DateTimeOffset(DateTime.Now.Year, 12, 1, 20, 0, 0, TimeSpan.Zero);
                    dateTo = new DateTimeOffset(DateTime.Now.Year, 12, 8, 20, 0, 0, TimeSpan.Zero);
                    break;
                case 2:
                    dateFrom = new DateTimeOffset(DateTime.Now.Year, 12, 8, 20, 0, 0, TimeSpan.Zero);
                    dateTo = new DateTimeOffset(DateTime.Now.Year, 12, 15, 20, 0, 0, TimeSpan.Zero);
                    break;
                case 3:
                    dateFrom = new DateTimeOffset(DateTime.Now.Year, 12, 15, 20, 0, 0, TimeSpan.Zero);
                    dateTo = new DateTimeOffset(DateTime.Now.Year, 12, 22, 20, 0, 0, TimeSpan.Zero);
                    break;
                default:
                    dateFrom = new DateTimeOffset(DateTime.Now.Year, 12, 1, 20, 0, 0, TimeSpan.Zero);
                    dateTo = new DateTimeOffset(DateTime.Now.Year, 12, 25, 20, 0, 0, TimeSpan.Zero);
                    break;
            }

            var correctAnswers = this._testAnswerRepository.GetCorrectAnswersPerUserForDateRange(dateFrom, dateTo);
            var wrongAnswers = this._testAnswerRepository.GetWrongAnswersPerUserForDateRange(dateFrom, dateTo);

            foreach (var result in results)
            {
                correctAnswers.TryGetValue(result.UserId, out var correctAnswersCount);
                result.CorrectAnswersCount = correctAnswersCount;

                wrongAnswers.TryGetValue(result.UserId, out var wrongAnswersCount);
                result.WrongAnswersCount = wrongAnswersCount;
            }

            return results;
        }
    }
}