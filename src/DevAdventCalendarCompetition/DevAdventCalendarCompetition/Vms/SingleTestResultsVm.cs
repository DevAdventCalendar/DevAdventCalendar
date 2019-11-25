using System;
using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Vms
{
    public class SingleTestResultsVm
    {
        public int TestNumber { get; set; }

        public List<SingleTestResultEntry> Entries { get; private set; }

        public List<TestResultEntryVm> TotalResults { get;  private set; }

        public bool TestEnded { get; set; }

        public TimeSpan TestOffset
        {
            get
            {
                if (this.EndDate.HasValue && this.StartDate.HasValue)
                {
                    return this.EndDate.Value.Subtract(this.StartDate.Value);
                }

                return TimeSpan.Zero;
            }
        }

        public DateTime? EndDate { get; set; }

        public DateTime? StartDate { get; set; }
    }
}