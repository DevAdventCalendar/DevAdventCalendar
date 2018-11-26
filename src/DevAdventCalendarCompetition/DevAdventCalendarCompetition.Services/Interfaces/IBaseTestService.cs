using DevAdventCalendarCompetition.Services.Models;
using System;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IBaseTestService
    {
        TestDto GetTestByNumber(int testNumber);

        void AddTestAnswer(int testId, string userId, DateTime testStartDate);

        TestAnswerDto GetAnswerByTestId(int testId);

        void AddTestWrongAnswer(string userId, int testId, string wrongAnswer, DateTime wrongAnswerDate);
    }
}