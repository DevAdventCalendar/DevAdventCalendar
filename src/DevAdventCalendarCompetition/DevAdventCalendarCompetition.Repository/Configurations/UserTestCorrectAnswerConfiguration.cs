using System;
using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DevAdventCalendarCompetition.Repository.Configurations
{
    public class UserTestCorrectAnswerConfiguration : IEntityTypeConfiguration<UserTestCorrectAnswer>
    {
        public void Configure(EntityTypeBuilder<UserTestCorrectAnswer> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.HasKey(ut => ut.Id);

            builder.Property(ut => ut.AnsweringTimeOffset).HasConversion(new TimeSpanToTicksConverter());

            builder.HasOne(ut => ut.Test)
                .WithMany(t => t.UserCorrectAnswers)
                .HasForeignKey(ut => ut.TestId)
                .IsRequired();

            builder.HasOne(ut => ut.User)
                .WithMany(u => u.CorrectAnswers)
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();
        }
    }
}