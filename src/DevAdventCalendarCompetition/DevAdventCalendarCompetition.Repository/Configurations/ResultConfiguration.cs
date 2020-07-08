using System;
using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevAdventCalendarCompetition.Repository.Configurations
{
    public class ResultConfiguration : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.HasOne(r => r.User)
                .WithOne()
                .HasForeignKey<Result>(r => r.UserId)
                .IsRequired();
        }
    }
}