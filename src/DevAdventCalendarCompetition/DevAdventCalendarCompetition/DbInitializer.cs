using DevAdventCalendarCompetition.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace DevAdventCalendarCompetition
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _context;

        public DbInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            _context.Database.Migrate();
        }
    }
}
