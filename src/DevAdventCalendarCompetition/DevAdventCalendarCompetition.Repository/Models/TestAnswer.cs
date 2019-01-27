using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevAdventCalendarCompetition.Repository.Models
{
    [Table("TestAnswer")]
    public class TestAnswer : ModelBase
    {
        [Required]
        [ForeignKey("Test")]
        public int TestId { get; set; }

        public Test Test { get; set; }

        [Required]
        [MaxLength(450)]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime AnsweringTime { get; set; }

        public TimeSpan AnsweringTimeOffset { get; set; }

        public string PlainAnswer { get; set; }
    }
}