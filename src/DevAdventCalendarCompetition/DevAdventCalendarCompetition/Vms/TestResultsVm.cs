using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Vms
{
    public class TestResultsVm
    {
        public int CurrentUserPosition { get; set; }

#pragma warning disable CA2227 // Collection properties should be read only
        public Dictionary<int, PaginatedCollection<TestResultEntryVm>> TotalTestResults { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
    }
}