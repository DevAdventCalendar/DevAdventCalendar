using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Vms
{
    public class TestResultsVm
    {
        public int CurrentUserPosition { get; set; }

        public List<SingleTestResultsVm> SingleTestResults { get; private set; }

        public PaginatedList<TotalTestResultEntryVm> TotalTestResults { get; private set; }
    }
}