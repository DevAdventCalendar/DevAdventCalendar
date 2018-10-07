using DevAdventCalendarCompetition.Repository.Context;

namespace DevAdventCalendarCompetition.Repository
{
    public class AccountRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AccountRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}