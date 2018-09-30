using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevAdventCalendarCompetition.Vms
{
    public class SingleTestResultsVm
    {
        public int TestNumber { get; set; }

        public List<SingleTestResultEntry> Entries { get; set; }

        public List<TotalTestResultEntryVm> TotalResults { get; set; }

        public bool TestEnded { get; set; }

        public TimeSpan TestOffset
        {
            get {
                if (EndDate.HasValue && StartDate.HasValue)
                {
                    return EndDate.Value.Subtract(StartDate.Value);
                }
                return TimeSpan.Zero;
            }
        }

        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }
    }
}