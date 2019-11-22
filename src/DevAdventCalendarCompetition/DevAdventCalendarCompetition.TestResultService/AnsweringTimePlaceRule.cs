using System;
using System.Collections.Generic;
using System.Linq;
using DevAdventCalendarCompetition.TestResultService.Interfaces;

namespace DevAdventCalendarCompetition.TestResultService
{
    public class AnsweringTimePlaceRule : ITestResultPlaceRule
    {
        public List<CompetitionResult> GetUsersOrder(List<CompetitionResult> users)
        {
            return users
                .OrderBy(u => u.AnsweringTimeOffset)
                .ToList();
        }
    }
}
