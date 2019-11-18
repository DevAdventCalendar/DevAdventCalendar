using System.Collections.Generic;

namespace DevAdventCalendarCompetition.TestResultService.Interfaces
{
    public interface ITestResultPlaceRule
    {
        List<CompetitionResult> GetUsersOrder(List<CompetitionResult> users);
    }
}
