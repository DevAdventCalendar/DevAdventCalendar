using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Interfaces;

namespace DevAdventCalendarCompetition.Repository
{
    public class ManageRepository : IManageRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ManageRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}