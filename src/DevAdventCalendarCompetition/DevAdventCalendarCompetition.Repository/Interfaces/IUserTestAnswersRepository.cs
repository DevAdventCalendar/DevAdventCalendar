using System;
using System.Collections.Generic;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.Repository.Interfaces
{
    public interface IUserTestAnswersRepository
    {
        UserTestCorrectAnswer GetCorrectAnswerByTestId(int testId);

        UserTestCorrectAnswer GetCorrectAnswerByUserId(string userId, int testId);

        int GetCorrectAnswersCountForUser(string userId);

        IDictionary<string, int> GetCorrectAnswersPerUserForDateRange(DateTimeOffset dateFrom, DateTimeOffset dateTo);

        IDictionary<string, int> GetWrongAnswersPerUserForDateRange(DateTimeOffset dateFrom, DateTimeOffset dateTo);

        bool HasUserAnsweredTest(string userId, int testId);

        void UpdateCorrectAnswer(UserTestCorrectAnswer testAnswer);

        void AddCorrectAnswer(UserTestCorrectAnswer testAnswer);

        void AddWrongAnswer(UserTestWrongAnswer wrongAnswer);
    }
}