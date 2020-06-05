using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Models.TestViewModels
{
    public class TestResultsVm
    {
        public int UserFinalPosition { get; set; }

        public int UserWeek1Position { get; set; }

        public int UserWeek2Position { get; set; }

        public int UserWeek3Position { get; set; }

#pragma warning disable CA2227 // Collection properties should be read only
        public Dictionary<int, PaginatedCollection<TestResultEntryVm>> TotalTestResults { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
    }
}