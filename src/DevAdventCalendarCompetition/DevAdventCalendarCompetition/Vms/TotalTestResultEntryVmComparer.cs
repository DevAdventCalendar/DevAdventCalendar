using System;
using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Vms
{
    public class TotalTestResultEntryVmComparer : IEqualityComparer<TotalTestResultEntryVm>
    {
        public bool Equals(TotalTestResultEntryVm x, TotalTestResultEntryVm y)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            return x.UserId == y.UserId;
#pragma warning restore CA1062 // Validate arguments of public methods
        }

        public int GetHashCode(TotalTestResultEntryVm obj)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            return obj.UserId.GetHashCode(StringComparison.CurrentCulture);
#pragma warning restore CA1062 // Validate arguments of public methods
        }
    }
}