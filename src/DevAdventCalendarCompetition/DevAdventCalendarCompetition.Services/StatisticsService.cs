using System;
using System.Collections.Generic;
using System.Text;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;

namespace DevAdventCalendarCompetition.Services
{
    public class StatisticsService : ITestStatisticsService
    {
        private readonly ITestStatisticsRepository _statisticsService;

        public StatisticsService(ITestStatisticsRepository testStatisticsRepository)
        {
            this._statisticsService = testStatisticsRepository;
        }

        public int GetWrongAnswerCount(string userID, int testID)
        {
            return this._statisticsService.GetUserTestWrongAnswerCount(userID, testID);
        }

        public DateTime GetCorrectAnswerDateTime(string userID, int testID)
        {
            return this._statisticsService.GetUserTestCorrectAnswerDate(userID, testID);
        }

        public int TmpStatisticsImpementation(string playerID, int testID)
        {
            this.GetWrongAnswerCount(playerID, testID);
            this.GetCorrectAnswerDateTime(playerID, testID);

            return int.Parse(playerID, System.Globalization.NumberStyles.HexNumber, null) + testID;
        }
    }
}
