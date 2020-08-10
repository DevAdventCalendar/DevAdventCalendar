using System.Collections.Generic;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IResultsService
    {
        UserPosition GetUserPosition(string userId);

        Dictionary<int, List<TestResultDto>> GetAllTestResults();
    }
}