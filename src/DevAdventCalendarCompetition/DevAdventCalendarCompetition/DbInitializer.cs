using DevAdventCalendarCompetition.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace DevAdventCalendarCompetition
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _context;

        public DbInitializer(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void Seed()
        {
            this._context.Database.Migrate();
        }
    }
}
