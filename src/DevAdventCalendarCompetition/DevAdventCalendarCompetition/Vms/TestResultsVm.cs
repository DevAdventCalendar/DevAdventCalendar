using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Vms
{
    public class TestResultsVm
    {
        public List<SingleTestResultsVm> SingleTestResults { get; set; }

        public List<TotalTestResultEntryVm> TotalTestResults { get; set; }
    }
}