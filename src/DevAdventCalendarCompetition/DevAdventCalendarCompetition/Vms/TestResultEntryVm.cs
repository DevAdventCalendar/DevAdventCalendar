using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Vms
{
    public class TestResultEntryVm
    {
        public string FullName { get; set; }

        public int Position { get; set; }

        public int CorrectAnswers { get; set; }

        public int WrongAnswers { get; set; }

        public int TotalPoints { get; set; }

        public string UserId { get; set; }

        public int Week1Points { get; set; }

        public int Week2Points { get; set; }

        public int Week3Points { get; set; }

        public int Week1Place { get; set; }

        public int Week2Place { get; set; }

        public int Week3Place { get; set; }

        public int FinalPoints { get; set; }

        public int FinalPlace { get; set; }
    }
}