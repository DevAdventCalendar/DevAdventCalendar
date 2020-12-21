using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Models.Test
{
    public class TestResultEntryViewModel
    {
        public string UserName { get; set; }

        public int Position { get; set; }

        public int CorrectAnswers { get; set; }

        public int WrongAnswers { get; set; }

        public string UserId { get; set; }

        public int? Week1Points { get; set; }

        public int? Week2Points { get; set; }

        public int? Week3Points { get; set; }

        public int? Week1Place { get; set; }

        public int? Week2Place { get; set; }

        public int? Week3Place { get; set; }

        public int? FinalPoints { get; set; }

        public int? FinalPlace { get; set; }

        public int? Week1TimeSum { get; set; }

        public int? Week2TimeSum { get; set; }

        public int? Week3TimeSum { get; set; }

        public int? FinalTimeSum { get; set; }
    }
}