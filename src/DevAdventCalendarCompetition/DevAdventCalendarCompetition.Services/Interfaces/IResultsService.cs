using System.Collections.Generic;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IResultsService
    {
        UserPosition GetUserPosition(string userId);

        List<TestResultDto> GetTestResults(int weekNumber, int pageCount, int pageIndex);

        int GetTotalTestResultsCount(int weekNumber);
    }
}