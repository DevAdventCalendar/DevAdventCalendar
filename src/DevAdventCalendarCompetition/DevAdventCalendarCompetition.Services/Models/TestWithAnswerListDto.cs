using System;
using System.Collections.Generic;
using DevAdventCalendarCompetition.Repository.Migrations;

namespace DevAdventCalendarCompetition.Services.Models
{
    public class TestWithAnswerListDto
    {
        public int TestId { get; set; }

        public int Number { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ICollection<TestAnswerDto> Answers { get; internal set; }

        public ICollection<TestWrongAnswerDto> WrongAnswers { get; internal set; }

        public bool HasEnded { get; set; }
    }
}