using System;

namespace DevAdventCalendarCompetition.Models.Manage
{
    public class DisplayStatisticsViewModel
    {
        public int WrongAnswerCount { get; set; }

        public DateTime? CorrectAnswerDate { get; set; }

        public int TestNumber { get; set; }
    }
}