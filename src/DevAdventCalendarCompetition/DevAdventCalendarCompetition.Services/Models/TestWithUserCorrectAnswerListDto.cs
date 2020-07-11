using System;
using System.Collections.Generic;
using DevAdventCalendarCompetition.Repository.Migrations;

namespace DevAdventCalendarCompetition.Services.Models
{
    public class TestWithUserCorrectAnswerListDto
    {
        public int TestId { get; set; }

        public int Number { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ICollection<UserTestCorrectAnswerDto> UserCorrectAnswers { get; internal set; }

        public ICollection<UserTestWrongAnswerDto> UserWrongAnswers { get; internal set; }

        public bool HasEnded { get; set; }
    }
}