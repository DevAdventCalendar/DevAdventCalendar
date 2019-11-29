using System;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.Services.Models
{
    public class TestDto
    {
#pragma warning disable CA1822
        public bool IsAdvent => true;

#pragma warning restore CA1822
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
    }
}