using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Services.Interfaces;

namespace DevAdventCalendarCompetition.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
        }

        public int Commit() => this._context.SaveChanges();
    }
}