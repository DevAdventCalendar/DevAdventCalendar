using System;

namespace DevAdventCalendarCompetition.Repository.Models
{
    public class TestAnswer : ModelBase
    {
        public int TestId { get; set; }

        public Test Test { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime AnsweringTime { get; set; }

        public TimeSpan AnsweringTimeOffset { get; set; }
    }

    public class TestAnswerMapping : MappingBase<TestAnswer>
    {
        public TestAnswerMapping()
        {
            // TODO fix mappings
            //HasRequired(el => el.Test)
            //    .WithMany(el => el.Answers)
            //    .HasForeignKey(el => el.TestId);

            //HasRequired(el => el.User)
            //    .WithMany(el => el.Answers)
            //    .HasForeignKey(el => el.UserId);
        }
    }
}