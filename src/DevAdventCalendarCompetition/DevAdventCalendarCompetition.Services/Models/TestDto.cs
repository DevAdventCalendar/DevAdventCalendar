using System;
using System.Collections.Generic;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.Services.Models
{
    public class TestDto
    {
#pragma warning disable CA1822
        public bool IsAdvent => DateTime.Now.Month == 12 && DateTime.Now.Day < 25;
#pragma warning restore CA1822

        public int Id { get; set; }

        public int Number { get; set; }

        public string Description { get; set; }

#pragma warning disable CA2227 // Collection properties should be read only
        public List<TestAnswerDto> Answers { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

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