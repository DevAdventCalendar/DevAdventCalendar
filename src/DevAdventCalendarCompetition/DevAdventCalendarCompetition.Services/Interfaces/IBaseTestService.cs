using DevAdventCalendarCompetition.Repository.Models;
using System;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IBaseTestService
    {
        Test GetTestByNumber(int testNumber);

        void AddTestAnswer(int testId, string userId, DateTime testStartDate);

        TestAnswer GetAnswerByTestId(int testId);
    }
}