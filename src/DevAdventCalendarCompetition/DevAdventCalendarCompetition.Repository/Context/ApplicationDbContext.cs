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
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}