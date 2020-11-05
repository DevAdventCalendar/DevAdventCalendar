using System;
using System.Collections.Generic;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IStatisticsService
    {
        public List<DisplayStatisticsDto> FillResultsWithTestStats(string userId);
    }
}
