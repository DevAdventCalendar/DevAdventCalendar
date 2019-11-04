using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevAdventCalendarCompetition.TestResultService
{
    
    class TestResultService : ITestResultPlaceRule, ITestResultPointsRule, ITestResultRepository
    {
        public void Calculate()
        {
            throw new NotImplementedException();
        }

        public Task GetAnsweringTimeSum(DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            throw new NotImplementedException();
        }

        public Task GetCorrectAnswersCount(DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            throw new NotImplementedException();
        }

        public void GetFinalResults()
        {
            throw new NotImplementedException();
        }

        public Task GetWeeklyResults(int WeekNumber)
        {
            throw new NotImplementedException();
        }

        public Task GetWrongAnswersCount(DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            throw new NotImplementedException();
        }

        public Task SaveUserFinalPlace(int userId, int Place)
        {
            throw new NotImplementedException();
        }

        public Task SaveUserFinalScore(int userId, int Place)
        {
            throw new NotImplementedException();
        }

        public Task SaveUserWeeklyPlace(int userId, int WeekNumber, int Place)
        {
            throw new NotImplementedException();
        }

        public Task SaveUserWeeklyScore(int userId, int WeekNumber, int Score)
        {
            throw new NotImplementedException();
        }

        void  CalculateResults(DateTimeOffset dateFrom, DateTimeOffset dateTo) { }

       void CalculateWeeklyResults(int WeekNumber) { }

        void SaveFinalResults() { }

        void ITestResultRepository.SaveFinalResults()
        {
            throw new NotImplementedException();
        }
    }

 
}