using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevAdventCalendarCompetition.Repository.Models
{
    public class UserTestWrongAnswer
    {
        public int Id { get; set; }

        public int TestId { get; set; }

        public Test Test { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime Time { get; set; }

        public string Answer { get; set; }
    }
}