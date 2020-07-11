using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevAdventCalendarCompetition.Repository.Models
{
    public class Test
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Description { get; set; }

        public string Discount { get; set; }

        public string SponsorName { get; set; }

        public Uri SponsorLogoUrl { get; set; }

        public Uri DiscountUrl { get; set; }

        public Uri DiscountLogoUrl { get; set; }

        public string DiscountLogoPath { get; set; }

#pragma warning disable CA2227 // Collection properties should be read only
        public ICollection<TestAnswer> HashedAnswers { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

#pragma warning disable CA2227 // Collection properties should be read only
        public ICollection<UserTestCorrectAnswer> UserCorrectAnswers { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

#pragma warning disable CA2227 // Collection properties should be read only
        public ICollection<UserTestWrongAnswer> UserWrongAnswers { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        public bool HasStarted
        {
            get
            {
                if (!this.StartDate.HasValue)
                {
                    return false;
                }

                return DateTime.Now > this.StartDate.Value;
            }
        }

        public bool HasEnded
        {
            get
            {
                if (!this.EndDate.HasValue)
                {
                    return false;
                }

                return DateTime.Now > this.EndDate.Value;
            }
        }

        public TestStatus Status
        {
            get
            {
                if (!this.HasStarted)
                {
                    return TestStatus.NotStarted;
                }

                if (!this.HasEnded)
                {
                    return TestStatus.Started;
                }

                return TestStatus.Ended;
            }
        }
    }
}