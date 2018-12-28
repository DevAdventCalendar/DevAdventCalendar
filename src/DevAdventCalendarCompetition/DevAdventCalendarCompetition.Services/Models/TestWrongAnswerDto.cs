using System;

namespace DevAdventCalendarCompetition.Services.Models
{
    public class TestWrongAnswerDto
    {
        public int TestId { get; set; }

        public string UserId { get; set; }

        public DateTime AnsweringTime { get; set; }

        public string Answer { get; set; }
    }
}