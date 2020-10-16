using System;
using System.Collections.Generic;
using System.Text;
using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;

namespace DevAdventCalendarCompetition.Services
{
    public class StatisticsService
    {
        private readonly ITestStatisticsService _statisticsService;

        public StatisticsService(ITestStatisticsService statisticsService)
        {
            this._statisticsService = statisticsService;
        }

        public int GetWrongAnswerCount(string playerID, int testID)
        {
            return this._statisticsService.GetUserTestWrongAnswerCount(playerID, testID);
        }

        public DateTime GetCorrectAnswerDate(string playerID, int testID)
        {
            return this._statisticsService.GetUserTestCorrectAnswerDate(playerID, testID);
        }

        public int TmpStatisticsImpementation(string playerID, int testID)
        {
            this.GetWrongAnswerCount(playerID, testID);
            this.GetCorrectAnswerDate(playerID, testID);

            return int.Parse(playerID, System.Globalization.NumberStyles.HexNumber, null) + testID;
        }
    }
}
