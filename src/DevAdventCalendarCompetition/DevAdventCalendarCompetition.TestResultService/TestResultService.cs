using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DevAdventCalendarCompetition.Services.Options;
using DevAdventCalendarCompetition.TestResultService.Interfaces;

namespace DevAdventCalendarCompetition.TestResultService
{
    public class TestResultService
    {
        private readonly ITestResultRepository _testResultRepository;
        private readonly ITestResultPointsRule _correctAnswersPointsRule;
        private readonly ITestResultPointsRule _bonusPointsRule;
        private readonly ITestResultPlaceRule _answeringTimePlaceRule;
        private readonly TestSettings _testSettings;

        public TestResultService(ITestResultRepository resultRepository,
            ITestResultPointsRule resultPointsRule,
            ITestResultPointsRule bonusPointsRule,
            ITestResultPlaceRule timePlaceRule,
            TestSettings testSettings)
        {
            this._testResultRepository = resultRepository;
            this._correctAnswersPointsRule = resultPointsRule;
            this._bonusPointsRule = bonusPointsRule;
            this._answeringTimePlaceRule = timePlaceRule;
            this._testSettings = testSettings;
        }

        public List<CompetitionResult> CalculateResults(DateTime dateFrom, DateTime dateTo)
        {
            // Collection for storing temporal results for specific time span

            List<CompetitionResult> results = new List<CompetitionResult>();

            // Get users

            var usersId = this._testResultRepository.GetUsersId(); // TODO: Update model - get distinct users

            foreach (var id in usersId)
            {
                var user = this._testResultRepository.GetUserById(id); // ToDo: Temporarily for debugging
                var correctAnswerDates = this._testResultRepository.GetCorrectAnswersDates(id, dateFrom, dateTo);
                var wrongAnswersCounts = this._testResultRepository.GetWrongAnswersCountPerDay(id, dateFrom, dateTo);
                var sumOffset = this._testResultRepository.GetAnsweringTimeSum(id, dateFrom, dateTo);

                var bonus = 0;
                foreach (var correctAnswerDate in correctAnswerDates)
                {
                    var wrongAnswers = wrongAnswersCounts.FirstOrDefault(w => w.TestStartDate.Date == correctAnswerDate.Date);
                    bonus += _bonusPointsRule.CalculatePoints(wrongAnswers != null ? wrongAnswers.Count : 0);
                }

                if (correctAnswerDates.Count() == 0)
                {
                    Console.WriteLine($"User did not answer any question.");
                }

                int overallPoints = _correctAnswersPointsRule.CalculatePoints(correctAnswerDates.Count()) + bonus;

                Console.WriteLine($"\nResults for user: { id } - correct answers: { correctAnswerDates.Count() }, bonus: { bonus }, offset: { sumOffset }... Overall points: { overallPoints }");

                results.Add(new CompetitionResult { UserId = id, Points = overallPoints, AnsweringTimeOffset = sumOffset });
            }

            return this._answeringTimePlaceRule.GetUsersOrder(results);
        }

        public void CalculateWeeklyResults(int weekNumber)
        {
            // Invoke CalculateResults with correct boundary dates according to weekNumber.

            // Save results to DB as weekly results.
            (int hour, int minute, int second) = GetStartHour();
            DateTime dateFrom = new DateTime(DateTime.Today.Year, 12, 1 + 7 * (weekNumber - 1), hour, minute, second);
            DateTime dateTo = dateFrom.AddDays(7);

            Console.WriteLine($"\nCurrent week number: { weekNumber }, dates from: { dateFrom.ToString(CultureInfo.InvariantCulture) }, to: { dateTo.ToString(CultureInfo.InvariantCulture) }");

            var userResults = CalculateResults(dateFrom, dateTo);

            foreach (var result in userResults)
            {
                _testResultRepository.SaveUserWeeklyScore(result.UserId, weekNumber, result.Points);
                _testResultRepository.SaveUserWeeklyPlace(result.UserId, weekNumber, result.Place);
            }
        }

        public void CalculateFinalResults()
        {
            // Save results to DB as weekly results.
            (int hour, int minute, int second) = GetStartHour();
            DateTime dateFrom = new DateTime(DateTime.Today.Year, 12, 1, hour, minute, second);
            DateTime dateTo = new DateTime(DateTime.Today.Year, 12, 24, 23, 59, 59);

            Console.WriteLine($"\nFinal results, dates from: { dateFrom.ToString(CultureInfo.InvariantCulture) }, to: { dateTo.ToString(CultureInfo.InvariantCulture) }");

            var userResults = CalculateResults(dateFrom, dateTo);

            foreach (var result in userResults)
            {
                _testResultRepository.SaveUserFinalScore(result.UserId, result.Points);
                _testResultRepository.SaveUserFinalPlace(result.UserId, result.Place);
            }
        }

        private (int, int, int) GetStartHour()
            => (this._testSettings.StartHour.Hours,
                this._testSettings.StartHour.Minutes,
                this._testSettings.StartHour.Seconds);
    }
}