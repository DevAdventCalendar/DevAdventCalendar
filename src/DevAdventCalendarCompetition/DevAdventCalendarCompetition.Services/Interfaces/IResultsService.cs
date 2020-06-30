using System.Collections.Generic;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IResultsService
    {
        string PrepareUserEmailForRODO(string email);

        UserPosition GetUserPosition(string userId);

        Dictionary<int, List<TestResultDto>> GetAllTestResults();
    }
}