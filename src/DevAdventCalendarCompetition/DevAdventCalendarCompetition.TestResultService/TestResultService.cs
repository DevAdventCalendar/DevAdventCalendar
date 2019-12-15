using System;
using System.Collections.Generic;
using System.Linq;
using DevAdventCalendarCompetition.TestResultService.Interfaces;

namespace DevAdventCalendarCompetition.TestResultService
{
    public class TestResultService
    {
        private readonly ITestResultRepository _testResultRepository;
        private readonly ITestResultPointsRule _correctAnswersPointsRule;
        private readonly ITestResultPointsRule _bonusPointsRule;
        private readonly ITestResultPlaceRule _answeringTimePlaceRule;

        public TestResultService(ITestResultRepository resultRepository,
            ITestResultPointsRule resultPointsRule,
            ITestResultPointsRule bonusPointsRule,
            ITestResultPlaceRule timePlaceRule)
        {
            this._testResultRepository = resultRepository;
            this._correctAnswersPointsRule = resultPointsRule;
            this._bonusPointsRule = bonusPointsRule;
            this._answeringTimePlaceRule = timePlaceRule;
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
                    var wrongAnswers = wrongAnswersCounts.FirstOrDefault(w => w.TestStartDate == correctAnswerDate);
                    bonus += _bonusPointsRule.CalculatePoints(wrongAnswers != null ? wrongAnswers.Count : 0);
                }

                int overallPoints = _correctAnswersPointsRule.CalculatePoints(correctAnswerDates.Count()) + bonus;

                Console.WriteLine($"\n\nResults for user: { id } - correct answers: { correctAnswerDates.Count() }, bonus: { bonus }, offset: { sumOffset }... Overall points: { overallPoints }");

                results.Add(new CompetitionResult { UserId = id, Points = overallPoints, AnsweringTimeOffset = sumOffset });
            }

            return this._answeringTimePlaceRule.GetUsersOrder(results);
        }

        public void CalculateWeeklyResults(int weekNumber)
        {
            // Invoke CalculateResults with correct boundary dates according to weekNumber.

            // Save results to DB as weekly results.

            DateTime dateFrom = new DateTime(DateTime.Today.Year, 12, 1 + 7 * (weekNumber - 1), 20, 0, 0);
            DateTime dateTo = dateFrom.AddDays(7);

            var userResults = CalculateResults(dateFrom, dateTo);

            foreach (var result in userResults)
            {
                _testResultRepository.SaveUserWeeklyScore(result.UserId, weekNumber, result.Points);
                _testResultRepository.SaveUserWeeklyPlace(result.UserId, weekNumber, result.Place);
            }
        }

        public void SaveFinalResults()
        {
            // Check results from all weeks and calculate final points and place for users.

            throw new NotImplementedException();
        }
    }
}