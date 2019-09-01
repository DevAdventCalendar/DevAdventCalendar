using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevAdventCalendarCompetition.Repository.Models
{
    public enum TestStatus
    {
        NotStarted,
        Started,
        Ended
    }

    [Table("Test")]

    public class Test : ModelBase
    {
        private DateTime? endDate;

        public int Number { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate
        {
            get
            {
                if (!this.endDate.HasValue)
                {
                    return null;
                }

                var value = DateTime.SpecifyKind(this.endDate.Value, DateTimeKind.Local);
                return value;
            }

            set
            {
                this.endDate = value;
            }
        }

#pragma warning disable CA2227 // Collection properties should be read only
        public ICollection<TestAnswer> Answers { get;  set; }
#pragma warning restore CA2227 // Collection properties should be read only

        public ICollection<TestWrongAnswer> WrongAnswers { get; private set; }

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

        public string SponsorName { get; set; }

        public Uri SponsorLogoUrl { get; set; }

        public string Description { get; set; }

        public string HashedAnswer { get; set; }

        public string Discount { get; set; }

        public Uri DiscountUrl { get; set; }

        public Uri DiscountLogoUrl { get; set; }

        public string DiscountLogoPath { get; set; }

        public string PlainAnswer { get; set; }
    }
}