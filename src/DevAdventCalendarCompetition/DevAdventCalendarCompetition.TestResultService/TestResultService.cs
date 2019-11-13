using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.TestResultService.Interfaces;

namespace DevAdventCalendarCompetition.TestResultService
{
    public class TestResultService
    {
        private readonly ITestResultRepository _testResultRepository;
        private ITestResultPointsRule _correctAnswersPointsRule;
        private ITestResultPointsRule _bonusPointsRule;
        private ITestResultPlaceRule _answeringTimePlaceRule;

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

        public async Task<List<CompetitionResult>> CalculateResults(DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            // Collection for storing temporal results for specific time span

            List<CompetitionResult> results = new List<CompetitionResult>();

            // Get users

            var usersId = await this._testResultRepository.GetUsersId(); // TODO: Update model - get distinct users

            foreach (var id in usersId)
            {
                var correctAnswersCount = await this._testResultRepository.GetCorrectAnswersCount(id, dateFrom, dateTo);
                var wrongAnswersCount = await this._testResultRepository.GetWrongAnswersCount(id, dateFrom, dateTo);
                var sumOffset = await this._testResultRepository.GetAnsweringTimeSum(id, dateFrom, dateTo);

                int overallPoints = _correctAnswersPointsRule.CalculatePoints(correctAnswersCount) +
                                    _bonusPointsRule.CalculatePoints(wrongAnswersCount);

                results.Add(new CompetitionResult { UserId = id, Points = overallPoints, AnsweringTimeOffset = sumOffset });
            }

            return this._answeringTimePlaceRule.GetUsersOrder(results);
        }

        public void CalculateWeeklyResults(int weekNumber)
        {
            // Invoke CalculateResults with correct boundary dates according to weekNumber.

            // Save results to DB as weekly results.

            throw new NotImplementedException();
        }

        public void SaveFinalResults()
        {
            // Check results from all weeks and calculate final points and place for users.

            throw new NotImplementedException();
        }
    }
}