using System;
using System.Threading.Tasks;

namespace DevAdventCalendarCompetition.TestResultService
{
    public interface ITestResultRepository
    {
        Task GetWrongAnswersCount(DateTimeOffset dateFrom, DateTimeOffset dateTo);
        Task GetAnsweringTimeSum(DateTimeOffset dateFrom , DateTimeOffset dateTo);
        Task GetCorrectAnswersCount(DateTimeOffset dateFrom, DateTimeOffset dateTo);
        Task GetWeeklyResults(int WeekNumber );
        void GetFinalResults();
        void SaveFinalResults();
        Task SaveUserWeeklyScore(int userId, int WeekNumber, int Score);
        Task SaveUserWeeklyPlace(int userId, int WeekNumber , int Place);
        Task SaveUserFinalScore(int userId, int Place); 
        Task SaveUserFinalPlace(int userId,int  Place );



    }
}
