using System;

namespace DevAdventCalendarCompetition.Repository.Models
{
    public class UserTestCorrectAnswer
    {
        public int Id { get; set; }

        public int TestId { get; set; }

        public Test Test { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime AnsweringTime { get; set; }

        public TimeSpan AnsweringTimeOffset { get; set; }
    }
}