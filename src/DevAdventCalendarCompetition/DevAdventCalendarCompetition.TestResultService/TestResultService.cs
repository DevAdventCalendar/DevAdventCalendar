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
        private ITestResultPointsRule _wrongAnswersPointsRule;

        public TestResultService()
        {
            this._testResultRepository = new TestResultRepository();
            this._correctAnswersPointsRule = new CorrectAnswerPointsRule();
            this._wrongAnswersPointsRule = new WrongAnswerPointsRule();
        }

        public async void CalculateResults(DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            // Get users

            var correctAnswersCount = await this._testResultRepository.GetCorrectAnswersCount(dateFrom, dateTo);
            var wrongAnswersCount = await this._testResultRepository.GetWrongAnswersCount(dateFrom, dateTo);

            int overallPoints = _correctAnswersPointsRule.CalculatePoints(correctAnswersCount) +
                                _wrongAnswersPointsRule.CalculatePoints(wrongAnswersCount);
        }

        public void CalculateWeeklyResults(int weekNumber)
        {
            // Invoke CalculateResults with correct boundary dates according to weekNumber.

            // Save results to DB as weekly results.
        }

        public void SaveFinalResults()
        {
            // Check results from all weeks and calculate final points and place for users.

            throw new NotImplementedException();
        }
    }

 
}