using System;

namespace DevAdventCalendarCompetition.Services.Models
{
    public class UserTestCorrectAnswerDto
    {
        public int TestId { get; set; }

        public string UserId { get; set; }

        public string UserFullName { get; set; }

        public DateTime AnsweringTime { get; set; }

        public TimeSpan AnsweringTimeOffset { get; set; }
    }
}