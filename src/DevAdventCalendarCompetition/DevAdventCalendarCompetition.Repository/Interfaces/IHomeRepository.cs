using System;
using System.Collections.Generic;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.Repository.Interfaces
{
    public interface IHomeRepository
    {
        Test GetCurrentTest();

        Test GetTestByNumber(int testNumber);

        TestAnswer GetTestAnswerByUserId(string userId, int testId);

        List<Test> GetAllTests();

        List<Test> GetTestsWithUserAnswers();

        int GetCorrectAnswersCountForUser(string userId);

        IDictionary<string, int> GetCorrectAnswersPerUserForDateRange(DateTimeOffset dateFrom, DateTimeOffset dateTo);

        IDictionary<string, int> GetWrongAnswersPerUserForDateRange(DateTimeOffset dateFrom, DateTimeOffset dateTo);

        UserPosition GetUserPosition(string userId);

        List<Result> GetTestResultsForWeek(int weekNumber);
    }
}