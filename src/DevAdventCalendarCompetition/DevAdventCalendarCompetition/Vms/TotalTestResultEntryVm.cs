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
}