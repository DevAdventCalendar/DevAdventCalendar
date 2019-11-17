using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.TestResultService.Interfaces
{
    public interface ITestResultRepository
    {
        string[] GetUsersId();
        int GetWrongAnswersCount(string userId, DateTimeOffset dateFrom, DateTimeOffset dateTo);
        int GetAnsweringTimeSum(string userId, DateTimeOffset dateFrom, DateTimeOffset dateTo);
        int GetCorrectAnswersCount(string userId, DateTimeOffset dateFrom, DateTimeOffset dateTo);
        List<Result> GetFinalResults();
        void SaveFinalResults();
        void SaveUserWeeklyScore(string userId, int weekNumber, int score);
        void SaveUserWeeklyPlace(string userId, int weekNumber, int place);
        void SaveUserFinalScore(string userId, int score); 
        void SaveUserFinalPlace(string userId, int place);
    }
}
