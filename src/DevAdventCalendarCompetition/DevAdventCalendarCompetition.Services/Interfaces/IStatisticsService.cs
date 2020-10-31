using System;
using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IStatisticsService
    {
        public int GetWrongAnswerCount(string userId, int testId);

        public DateTime? GetCorrectAnswerDateTime(string userId, int testId);
    }
}
