using System;
using System.Collections.Generic;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.Repository.Interfaces
{
    public interface ITestAnswerRepository
    {
        TestAnswer GetAnswerByTestId(int testId);

        TestAnswer GetTestAnswerByUserId(string userId, int testId);

        int GetCorrectAnswersCountForUser(string userId);

        IDictionary<string, int> GetCorrectAnswersPerUserForDateRange(DateTimeOffset dateFrom, DateTimeOffset dateTo);

        IDictionary<string, int> GetWrongAnswersPerUserForDateRange(DateTimeOffset dateFrom, DateTimeOffset dateTo);

        bool HasUserAnsweredTest(string userId, int testId);

        void UpdateAnswer(TestAnswer testAnswer);

        void AddAnswer(TestAnswer testAnswer);

        void AddWrongAnswer(TestWrongAnswer wrongAnswer);

        void DeleteTestAnswers();
    }
}