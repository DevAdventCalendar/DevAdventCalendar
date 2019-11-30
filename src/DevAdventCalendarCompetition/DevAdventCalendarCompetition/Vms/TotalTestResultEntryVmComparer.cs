using System;
using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Vms
{
    public class TotalTestResultEntryVmComparer : IEqualityComparer<TestResultEntryVm>
    {
        public bool Equals(TestResultEntryVm x, TestResultEntryVm y)
        {
            if (x != null && y != null)
            {
                return x.UserId == y.UserId;
            }

            return false;
        }

        public int GetHashCode(TestResultEntryVm obj)
        {
            if (obj != null)
            {
                return obj.UserId.GetHashCode(StringComparison.CurrentCulture);
            }

            return 0;
        }
    }
}