using System;

namespace DevAdventCalendarCompetition.Services.Models
{
    public class UserTestWrongAnswerDto
    {
        public int TestId { get; set; }

        public string UserId { get; set; }

        public string UserFullName { get; set; }

        public DateTime AnsweringTime { get; set; }

        public string Answer { get; set; }
    }
}