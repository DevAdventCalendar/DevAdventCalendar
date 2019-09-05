using System;
using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Vms
{
    public class TotalTestResultEntryVmComparer : IEqualityComparer<TotalTestResultEntryVm>
    {
        public bool Equals(TotalTestResultEntryVm x, TotalTestResultEntryVm y)
        {
            if (x != null && y != null)
                {
                return x.UserId == y.UserId;
            }
        }

        public int GetHashCode(TotalTestResultEntryVm obj)
        {
            if (obj != null)
            {
                return obj.UserId.GetHashCode(StringComparison.CurrentCulture);
            }
        }
    }
}