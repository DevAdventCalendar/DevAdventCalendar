using System;
using System.Collections.Generic;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.TestResultService.Interfaces
{
    public interface ITestResultRepository
    {
        string[] GetUsersId();
        int[] GetWrongAnswersCountPerDay(string userId, DateTimeOffset dateFrom, DateTimeOffset dateTo);
        double GetAnsweringTimeSum(string userId, DateTimeOffset dateFrom, DateTimeOffset dateTo);
        int GetCorrectAnswersCount(string userId, DateTimeOffset dateFrom, DateTimeOffset dateTo);
        List<Result> GetFinalResults();
        void SaveUserWeeklyScore(string userId, int weekNumber, int score);
        void SaveUserWeeklyPlace(string userId, int weekNumber, int place);
        void SaveUserFinalScore(string userId, int score); 
        void SaveUserFinalPlace(string userId, int place);
    }
}
