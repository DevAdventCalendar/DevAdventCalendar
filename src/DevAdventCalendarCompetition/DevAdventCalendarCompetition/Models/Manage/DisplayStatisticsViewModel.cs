using System;

namespace DevAdventCalendarCompetition.Models.Manage
{
    public class DisplayStatisticsViewModel
    {
        public int WrongAnswerCount { get; set; }

        public string CorrectAnswerDateMessage { get; set; }

        public int TestId { get; set; }
    }
}