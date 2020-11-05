using System;

namespace DevAdventCalendarCompetition.Services.Models
{
    public class DisplayStatisticsDto
    {
        public int WrongAnswerCount { get; set; }

        public DateTime? CorrectAnswerDateTime { get; set; }

        // test phase
        public string StatusMessage { get; set; }

        // test phase
        public string WrongAnswerCountMessage { get; set; }

        // test phase
        public string CorrectAnswerDateMessage { get; set; }

        // test phase
        public int TestId { get; set; }
    }
}