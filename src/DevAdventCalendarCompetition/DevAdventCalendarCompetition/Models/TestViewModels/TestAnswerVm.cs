using System;

namespace DevAdventCalendarCompetition.Models.TestViewModels
{
    public class TestAnswerVm
    {
        public int TestId { get; set; }

        public string UserId { get; set; }

        public DateTime AnsweringTime { get; set; }

        public TimeSpan AnsweringTimeOffset { get; set; }
    }
}