using System;

namespace DevAdventCalendarCompetition.Services.Models
{
    public class StatisticsDto
    {
        public int WrongAnswerCount { get; set; }

        public DateTime? CorrectAnswerDateTime { get; set; }

        public int TestNumber { get; set; }
    }
}