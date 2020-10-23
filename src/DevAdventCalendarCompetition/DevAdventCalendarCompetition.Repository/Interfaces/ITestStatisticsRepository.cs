using System;
using System.Collections.Generic;
using System.Text;

namespace DevAdventCalendarCompetition.Repository.Interfaces
{
    public interface ITestStatisticsRepository
    {
        public int GetUserTestWrongAnswerCount(string userID, int testID);

        public DateTime? GetUserTestCorrectAnswerDate(string userID, int testID);
    }
}
