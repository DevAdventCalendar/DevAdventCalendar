using System;
using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface ITestStatisticsService
    {
        public int GetUserTestWrongAnswerCount(string userId, int testId);

        public DateTime GetUserTestCorrectAnswerDate(string userId, int testId);
    }
}