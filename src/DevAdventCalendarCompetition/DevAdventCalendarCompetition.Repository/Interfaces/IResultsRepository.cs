using System;
using System.Collections.Generic;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.Repository.Interfaces
{
    public interface IResultsRepository
    {
        UserPosition GetUserPosition(string userId);

        List<Result> GetTestResultsForWeek(int weekNumber, int resultsCountToGet, int paginationIndex);

        int GetTotalTestResultsCount(int weekNumber);
    }
}