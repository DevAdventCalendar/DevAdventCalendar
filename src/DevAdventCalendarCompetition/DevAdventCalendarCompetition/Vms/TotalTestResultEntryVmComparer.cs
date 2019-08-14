using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Vms
{

    public class TotalTestResultEntryVmComparer : IEqualityComparer<TotalTestResultEntryVm>
    {
        public bool Equals(TotalTestResultEntryVm x, TotalTestResultEntryVm y)
        {
            return x.UserId == y.UserId;
        }

        public int GetHashCode(TotalTestResultEntryVm obj)
        {
            return obj.UserId.GetHashCode();
        }
    }
}