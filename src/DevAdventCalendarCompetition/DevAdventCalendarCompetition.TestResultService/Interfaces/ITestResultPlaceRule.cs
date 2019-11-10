using System;
using System.Collections.Generic;
using System.Text;

namespace DevAdventCalendarCompetition.TestResultService.Interfaces
{
    public interface ITestResultPlaceRule
    {
        List<CompetitionResult> GetUsersOrder(List<CompetitionResult> users);
    }
}
