using System;

namespace DevAdventCalendarCompetition.Models.Test
{
    public class TestAnswerViewModel
    {
        public int TestId { get; set; }

        public string UserId { get; set; }

        public DateTime AnsweringTime { get; set; }

        public TimeSpan AnsweringTimeOffset { get; set; }
    }
}