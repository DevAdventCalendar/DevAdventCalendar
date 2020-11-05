using System;
using System.Collections.Generic;
using System.Text;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
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

        // TODO: unfix max testId
        public List<DisplayStatisticsDto> FillResultsWithTestStats(string userId)
        {
            List<DisplayStatisticsDto> allCurrentStats = new List<DisplayStatisticsDto>();

            for (int currentTestId = 0; currentTestId < 24; currentTestId++)
            {
                allCurrentStats.Add(new DisplayStatisticsDto()
                {
                    CorrectAnswerDateTime = this._statisticsService.GetUserTestCorrectAnswerDate(userId, currentTestId),
                    WrongAnswerCount = this._statisticsService.GetUserTestWrongAnswerCount(userId, currentTestId),
                    TestId = currentTestId + 1
                });
            }

            return allCurrentStats;
        }
    }
}
