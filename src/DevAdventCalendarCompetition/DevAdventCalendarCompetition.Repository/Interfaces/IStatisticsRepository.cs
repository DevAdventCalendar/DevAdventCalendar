using System;
using System.Collections.Generic;
using System.Text;

namespace DevAdventCalendarCompetition.Repository.Interfaces
{
    public interface IStatisticsRepository
    {
        public int GetUserTestWrongAnswerCount(string userId, int testId);

        public DateTime? GetUserTestCorrectAnswerDate(string userId, int testId);
    }
}
