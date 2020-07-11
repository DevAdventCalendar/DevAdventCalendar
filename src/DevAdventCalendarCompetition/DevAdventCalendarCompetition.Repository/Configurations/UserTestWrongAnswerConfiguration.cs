using System;
using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevAdventCalendarCompetition.Repository.Configurations
{
    public class UserTestWrongAnswerConfiguration : IEntityTypeConfiguration<UserTestWrongAnswer>
    {
        public void Configure(EntityTypeBuilder<UserTestWrongAnswer> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.HasKey(ut => ut.Id);

            builder.HasOne(ut => ut.Test)
                .WithMany(t => t.UserWrongAnswers)
                .HasForeignKey(ut => ut.TestId)
                .IsRequired();

            builder.HasOne(ut => ut.User)
                .WithMany(u => u.WrongAnswers)
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

            builder.Property(ut => ut.Answer).IsRequired();
        }
    }
}
