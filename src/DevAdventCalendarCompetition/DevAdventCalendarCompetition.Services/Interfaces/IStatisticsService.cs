using System;
using System.Collections.Generic;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IStatisticsService
    {
        public List<StatisticsDto> FillResultsWithTestStats(string userId);

        public int GetHighestTestNumber(string userId);
    }
}
