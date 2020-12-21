using System;
using System.Collections.Generic;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.TestResultService.Interfaces
{
    public interface ITestResultRepository
    {
        string[] GetUsersId();
        IEnumerable<WrongAnswerData> GetWrongAnswersCountPerDay(string userId, DateTime dateFrom, DateTime dateTo);
        double GetAnsweringTimeSum(string userId, DateTime dateFrom, DateTime dateTo);
        IEnumerable<DateTime> GetCorrectAnswersDates(string userId, DateTime dateFrom, DateTime dateTo);
        List<Result> GetFinalResults();
        void SaveUserWeeklyScore(string userId, int weekNumber, int score);
        void SaveUserWeeklyPlace(string userId, int weekNumber, int place);
        void SaveUserFinalScore(string userId, int score); 
        void SaveUserFinalPlace(string userId, int place);
        ApplicationUser GetUserById(string id);
    }
}
