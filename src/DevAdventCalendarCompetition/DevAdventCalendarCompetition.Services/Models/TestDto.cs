using System;

namespace DevAdventCalendarCompetition.Services.Models
{
    public class TestDto
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public TestStatus Status { get; set; }
    }

    public enum TestStatus
    {
        NotStarted, Started, Ended
    }
}