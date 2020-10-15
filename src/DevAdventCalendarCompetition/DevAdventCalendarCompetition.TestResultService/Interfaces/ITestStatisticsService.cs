using System;
using System.Collections.Generic;

namespace DevAdventCalendarCompetition.TestResultService.Interfaces
{
    public interface ITestStatisticsService
    {
        public int GetWrongAnswersToTest(string userId, int testId);
        public DateTime GetCorrectAnswerDate(string userId, int testId);

    }
}