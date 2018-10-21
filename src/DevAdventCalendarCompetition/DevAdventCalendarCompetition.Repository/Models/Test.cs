using System;
using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Repository.Models
{
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

        public bool HasStarted => StartDate.HasValue;

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
    }

    public enum TestStatus
    {
        NotStarted, Started, Ended
    }
}