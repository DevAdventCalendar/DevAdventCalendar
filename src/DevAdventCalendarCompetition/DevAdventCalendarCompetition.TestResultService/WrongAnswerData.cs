using System;

namespace DevAdventCalendarCompetition.TestResultService
{
    public class WrongAnswerData
    {
        public DateTime TestStartDate { get; }
        public int Count { get; }

        public WrongAnswerData(DateTime testStartDate, int count)
        {
            this.TestStartDate = testStartDate;
            this.Count = count;
        }
    }
}