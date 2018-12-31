using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevAdventCalendarCompetition.Repository.Models
{
    [Table("Test")]
    public class Test : ModelBase
    {
        public int Number { get; set; }

        public DateTime? StartDate { get; set; }

        private DateTime? _endDate;

        public DateTime? EndDate
        {
            get
            {
                if (!_endDate.HasValue)
                    return null;

                var value = DateTime.SpecifyKind(_endDate.Value, DateTimeKind.Local);

                return value;
            }
            set { _endDate = value; }
        }

        public ICollection<TestAnswer> Answers { get; set; }

        public bool HasStarted
        {
            get
            {
                if (!StartDate.HasValue)
                    return false;

                return DateTime.Now > StartDate.Value;
            }
        }

        public bool HasEnded
        {
            get
            {
                if (!EndDate.HasValue)
                    return false;

                return DateTime.Now > EndDate.Value;
            }
        }

        public TestStatus Status
        {
            get
            {
                if (!HasStarted)
                    return TestStatus.NotStarted;
                if (!HasEnded)
                    return TestStatus.Started;
                return TestStatus.Ended;
            }
        }

        public string SponsorName { get; set; }

        public string SponsorLogoUrl { get; set; }

        public string Description { get; set; }

        public string HashedAnswer { get; set; }

        public string Discount { get; set; }

        public string DiscountUrl { get; set; }

        public string DiscountLogoUrl { get; set; }

        public string DiscountLogoPath { get; set; }

        public string PlainAnswer { get; set; }
    }

    public enum TestStatus
    {
        NotStarted, Started, Ended
    }
}