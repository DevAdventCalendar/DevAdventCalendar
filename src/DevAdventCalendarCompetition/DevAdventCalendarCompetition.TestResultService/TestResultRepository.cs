using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.TestResultService.Interfaces;

namespace DevAdventCalendarCompetition.TestResultService
{
    public class TestResultRepository : ITestResultRepository
    {
        public Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetAnsweringTimeSum(string userId, DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCorrectAnswersCount(string userId, DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            throw new NotImplementedException();
        }

        public void GetFinalResults()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetWrongAnswersCount(string userId, DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            throw new NotImplementedException();
        }

        public void SaveFinalResults()
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
    }
}
