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
                .OrderByDescending(u => u.Points)
                    .ThenBy(u => u.AnsweringTimeOffset)
                .Select((r, index) => new CompetitionResult 
                    { 
                        UserId = r.UserId, 
                        Points = r.Points, 
                        AnsweringTimeOffset = r.AnsweringTimeOffset, 
                        Place = index + 1 })
                .ToList();
        }
    }
}
