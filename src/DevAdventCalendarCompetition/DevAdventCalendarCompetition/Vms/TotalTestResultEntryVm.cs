using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Vms
{
    public class TotalTestResultEntryVm
    {
        public string FullName { get; set; }

        public int Position { get; set; }

        public int CorrectAnswers { get; set; }

        public int WrongAnswers { get; set; }

        public int TotalPoints { get; set; }

        public string UserId { get; set; }
    }

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