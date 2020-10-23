using System;
using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface ITestStatisticsService
    {
        public int GetWrongAnswerCount(string userID, int testID);

        public DateTime? GetCorrectAnswerDateTime(string userID, int testID);
    }
}
