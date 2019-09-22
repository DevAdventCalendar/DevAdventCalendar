using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Vms
{
    public class TestResultsVm
    {
        public int CurrentUserPosition { get; set; }

        public List<SingleTestResultsVm> SingleTestResults { get; private set; }

#pragma warning disable CA2227 // Collection properties should be read only
        public PaginatedCollection<TotalTestResultEntryVm> TotalTestResults { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
    }
}