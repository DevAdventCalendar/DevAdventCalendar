using System;

namespace DevAdventCalendarCompetition.Services.Models
{
    public class TestDto
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public string Description { get; set; }

        public string Answer { get; set; }

        public string SponsorLogoUrl { get; set; }

        public string SponsorName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public TestStatus Status { get; set; }
        public bool HasUserAnswered { get; set; }
    }

    public enum TestStatus
    {
        NotStarted, Started, Ended
    }
}