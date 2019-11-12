using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.TestResultService.Interfaces;

namespace DevAdventCalendarCompetition.TestResultService
{
    public class TestResultService
    {
        private ITestResultRepository _testResultRepository;
        private ITestResultPointsRule _correctAnswersPointsRule;
        private ITestResultPointsRule _bonusPointsRule;
        private ITestResultPlaceRule _answeringTimePlaceRule;

        public TestResultService()
        {
            this._testResultRepository = new TestResultRepository();
            this._correctAnswersPointsRule = new CorrectAnswerPointsRule();
            this._bonusPointsRule = new BonusPointsRule();
            this._answeringTimePlaceRule = new AnsweringTimePlaceRule();
        }

        public async Task<List<CompetitionResult>> CalculateResults(DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            // Collection for storing temporal results for specific time span

            List<CompetitionResult> results = new List<CompetitionResult>();

            // Get users

            var users = await this._testResultRepository.GetUsers(); // TODO: Update model - get distinct users

            foreach (var user in users)
            {
                var correctAnswersCount = await this._testResultRepository.GetCorrectAnswersCount(user.Id, dateFrom, dateTo);
                var wrongAnswersCount = await this._testResultRepository.GetWrongAnswersCount(user.Id, dateFrom, dateTo);
                var sumOffset = await this._testResultRepository.GetAnsweringTimeSum(user.Id, dateFrom, dateTo);

                int overallPoints = _correctAnswersPointsRule.CalculatePoints(correctAnswersCount) +
                                    _bonusPointsRule.CalculatePoints(wrongAnswersCount);

                results.Add(new CompetitionResult { UserId = user.Id, Points = overallPoints, AnsweringTimeOffset = sumOffset });
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