using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevAdventCalendarCompetition.Repository.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //TODO: load from config file
        private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-DevAdventCalendarCompetition-33CBD544-AB08-45ED-A959-D039E993B165;Trusted_Connection=True;MultipleActiveResultSets=true";

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}