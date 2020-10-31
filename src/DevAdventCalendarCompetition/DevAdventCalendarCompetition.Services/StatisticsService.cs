using System;
using System.Collections.Generic;
using System.Text;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;

namespace DevAdventCalendarCompetition.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _statisticsService;

        public StatisticsService(IStatisticsRepository statisticsRepository)
        {
            this._statisticsService = statisticsRepository;
        }

        public int GetWrongAnswerCount(string userId, int testId)
        {
            return this._statisticsService.GetUserTestWrongAnswerCount(userId, testId);
        }

        public DateTime? GetCorrectAnswerDateTime(string userId, int testId)
        {
            return this._statisticsService.GetUserTestCorrectAnswerDate(userId, testId);
        }
    }
}
