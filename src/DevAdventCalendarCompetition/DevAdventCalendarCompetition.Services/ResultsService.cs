using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using DevAdventCalendarCompetition.Services.Options;

namespace DevAdventCalendarCompetition.Services
{
    public class ResultsService : IResultsService
    {
        private readonly IResultsRepository _resultsRepository;
        private readonly IUserTestAnswersRepository _testAnswerRepository;
        private readonly IMapper _mapper;
        private readonly TestSettings _testSettings;

        public ResultsService(
            IResultsRepository resultsRepository,
            IUserTestAnswersRepository testAnswerRepository,
            IMapper mapper,
            TestSettings testSettings)
        {
            this._resultsRepository = resultsRepository;
            this._testAnswerRepository = testAnswerRepository;
            this._mapper = mapper;
            this._testSettings = testSettings;
        }

        public List<TestResultDto> GetTestResults(int weekNumber, int pageCount, int pageIndex)
        {
            var dbResults = this._resultsRepository.GetTestResultsForRanking(weekNumber, pageCount, pageIndex);

            if (dbResults != null && dbResults.Count > 0)
            {
                var results = dbResults.Select(x =>
                {
                    var result = this._mapper.Map<TestResultDto>(x);
                    AssignPointsAndPosition(weekNumber, x, result);
                    return result;
                }).ToList();

                return this.FillResultsWithAnswersStats(weekNumber, results);
            }

            return null;
        }

        public int GetTotalTestResultsCount(int weekNumber)
        {
            return this._resultsRepository.GetTotalTestResultsCount(weekNumber);
        }

        public UserPosition GetUserPosition(string userId)
        {
            return this._resultsRepository.GetUserPosition(userId);
        }

        private static void AssignPointsAndPosition(int weekNumber, Result x, TestResultDto result)
        {
            switch (weekNumber)
            {
                case 1:
                    result.Position = x.Week1Place ?? 0;
                    result.Points = x.Week1Points ?? 0;
                    break;
                case 2:
                    result.Position = x.Week2Place ?? 0;
                    result.Points = x.Week2Points ?? 0;
                    break;
                case 3:
                    result.Position = x.Week3Place ?? 0;
                    result.Points = x.Week3Points ?? 0;
                    break;
                case 4:
                    result.Position = x.FinalPlace ?? 0;
                    result.Points = x.FinalPoints ?? 0;
                    break;
            }
        }

        private (int, int, int) GetStartHour()
            => (this._testSettings.StartHour.Hours,
                this._testSettings.StartHour.Minutes,
                this._testSettings.StartHour.Seconds);

        private List<TestResultDto> FillResultsWithAnswersStats(int weekNumber, List<TestResultDto> results)
        {
            DateTimeOffset dateFrom;
            DateTimeOffset dateTo;

            (int hour, int minute, int second) = this.GetStartHour();

            switch (weekNumber)
            {
                case 1:
                    dateFrom = new DateTimeOffset(DateTime.Now.Year, 12, 1, hour, minute, second, TimeSpan.Zero);
                    dateTo = new DateTimeOffset(DateTime.Now.Year, 12, 8, hour, minute, second, TimeSpan.Zero);
                    break;
                case 2:
                    dateFrom = new DateTimeOffset(DateTime.Now.Year, 12, 8, hour, minute, second, TimeSpan.Zero);
                    dateTo = new DateTimeOffset(DateTime.Now.Year, 12, 15, hour, minute, second, TimeSpan.Zero);
                    break;
                case 3:
                    dateFrom = new DateTimeOffset(DateTime.Now.Year, 12, 15, hour, minute, second, TimeSpan.Zero);
                    dateTo = new DateTimeOffset(DateTime.Now.Year, 12, 22, hour, minute, second, TimeSpan.Zero);
                    break;
                default:
                    dateFrom = new DateTimeOffset(DateTime.Now.Year, 12, 1, hour, minute, second, TimeSpan.Zero);
                    dateTo = new DateTimeOffset(DateTime.Now.Year, 12, 25, hour, minute, second, TimeSpan.Zero);
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