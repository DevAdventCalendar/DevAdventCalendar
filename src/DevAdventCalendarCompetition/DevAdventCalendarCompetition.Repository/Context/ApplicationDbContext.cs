using System;
using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevAdventCalendarCompetition.Repository.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }

        public DbSet<Test> Test { get; set; }

        public DbSet<TestAnswer> TestAnswer { get; set; }

        public DbSet<TestWrongAnswer> TestWrongAnswer { get; set; }

        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Entity<Test>()
                .Property(t => t.SponsorLogoUrl)
                .HasConversion(v => v.ToString(), v => new Uri(v, UriKind.RelativeOrAbsolute));

            builder.Entity<Test>()
                .Property(t => t.DiscountUrl)
                .HasConversion(v => v.ToString(), v => new Uri(v, UriKind.RelativeOrAbsolute));

            builder.Entity<Test>()
                .Property(t => t.DiscountLogoUrl)
                .HasConversion(v => v.ToString(), v => new Uri(v, UriKind.RelativeOrAbsolute));

            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}