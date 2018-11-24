using System;
using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Services.Models
{
    public class TestWithAnswerListDto
    {
        public int TestId { get; set; }

        public int Number { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ICollection<TestAnswerDto> Answers { get; set; }

        public bool HasEnded { get; set; }
    }
}