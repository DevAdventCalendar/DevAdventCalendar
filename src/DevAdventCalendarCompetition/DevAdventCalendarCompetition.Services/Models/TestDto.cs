using System;

namespace DevAdventCalendarCompetition.Services.Models
{
    public enum TestStatus
    {
#pragma warning disable SA1602 // Enumeration items should be documented
        NotStarted,

        Started,

        Ended
#pragma warning restore SA1602 // Enumeration items should be documented
    }

    public class TestDto
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public string Description { get; set; }

        public string Answer { get; set; }

        public string PlainAnswer { get; set; }

        public Uri SponsorLogoUrl { get; set; }

        public string SponsorName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public TestStatus Status { get; set; }

        public string Discount { get; set; }

        public Uri DiscountUrl { get; set; }

        public Uri DiscountLogoUrl { get; set; }

        public string DiscountLogoPath { get; set; }

        public bool HasUserAnswered { get; set; }

        public bool IsAdvent => DateTime.Now.Month == 12 && DateTime.Now.Day < 25;

        public static implicit operator TestDto(TestDto v)
        {
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
            throw new NotImplementedException();
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
        }

        public TestDto ToTestDto()
        {
            throw new NotImplementedException();
        }
    }
}