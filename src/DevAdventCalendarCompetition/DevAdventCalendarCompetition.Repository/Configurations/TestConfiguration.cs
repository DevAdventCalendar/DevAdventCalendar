using System;
using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevAdventCalendarCompetition.Repository.Configurations
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.HasKey(t => t.Id);

            builder.OwnsMany(t => t.HashedAnswers, b =>
            {
                b.ToTable("TestAnswers");
                b.HasKey(ta => ta.Id);
            });

            builder
                .Property(t => t.SponsorLogoUrl)
                .HasConversion(v => v.ToString(), v => new Uri(v, UriKind.RelativeOrAbsolute));

            builder
                .Property(t => t.DiscountUrl)
                .HasConversion(v => v.ToString(), v => new Uri(v, UriKind.RelativeOrAbsolute));

            builder
                .Property(t => t.DiscountLogoUrl)
                .HasConversion(v => v.ToString(), v => new Uri(v, UriKind.RelativeOrAbsolute));
        }
    }
}
