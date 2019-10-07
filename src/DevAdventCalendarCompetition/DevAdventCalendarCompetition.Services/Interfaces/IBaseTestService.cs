using System;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IBaseTestService
    {
        TestDto GetTestByNumber(int testNumber);

        bool HasUserAnsweredTest(string userId, int testNumber);

        void AddTestAnswer(int testId, string userId, DateTime testStartDate);

        TestAnswerDto GetAnswerByTestId(int testId);

        void AddTestWrongAnswer(string userId, int testId, string wrongAnswer, DateTime wrongAnswerDate);

        bool VerifyTestAnswer(string userAnswer, string correntAnswer);
    }
}