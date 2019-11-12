using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.TestResultService.Interfaces
{
    public interface ITestResultRepository
    {
        Task<string[]> GetUsersId();
        Task<int> GetWrongAnswersCount(string userId, DateTimeOffset dateFrom, DateTimeOffset dateTo);
        Task<int> GetAnsweringTimeSum(string userId, DateTimeOffset dateFrom , DateTimeOffset dateTo);
        Task<int> GetCorrectAnswersCount(string userId, DateTimeOffset dateFrom, DateTimeOffset dateTo);
        void GetFinalResults();
        void SaveFinalResults();
        Task SaveUserWeeklyScore(int userId, int WeekNumber, int Score);
        Task SaveUserWeeklyPlace(int userId, int WeekNumber , int Place);
        Task SaveUserFinalScore(int userId, int Place); 
        Task SaveUserFinalPlace(int userId,int  Place );
    }
}
