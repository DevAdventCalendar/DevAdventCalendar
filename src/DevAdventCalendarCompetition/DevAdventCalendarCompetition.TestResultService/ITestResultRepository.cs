using System;
using System.Threading.Tasks;

namespace DevAdventCalendarCompetition.TestResultService
{
    interface ITestResultRepository
    {
        GetWrongAnswersCount(dateFrom: DateTimeOffset : dateTo: DateTimeOffset);
        GetAnsweringTimeSum(dateFrom: DateTimeOffset : dateTo: DateTimeOffset);
        GetCorrectAnswersCount(dateFrom: DateTimeOffset : dateTo: DateTimeOffset);
        GetWeeklyResults(WeekNumber int);
        GetFinalResults()
        SaveFinalResults()
        SaveUserWeeklyScore(userId int, WeekNumber int, Score int);
        SaveUserWeeklyPlace(userId int, WeekNumber int, Place int);
        SaveUserFinalScore(userId int, Place int);
        SaveUserFinalPlace(userId int, Place int);



    }
}
