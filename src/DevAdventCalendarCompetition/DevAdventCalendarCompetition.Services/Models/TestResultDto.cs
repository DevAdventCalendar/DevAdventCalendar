using System;
using System.Collections.Generic;
using System.Text;

namespace DevAdventCalendarCompetition.Services.Models
{
    public class TestResultDto
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public int CorrectAnswersCount { get; set; }

        public int WrongAnswersCount { get; set; }

        public int Points { get; set; }

        public int Position { get; set; }

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