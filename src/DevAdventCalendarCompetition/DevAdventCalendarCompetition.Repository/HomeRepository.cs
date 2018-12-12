using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DevAdventCalendarCompetition.Repository
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Test GetCurrentTest()
        {
            return _dbContext.Set<Test>().FirstOrDefault(el => el.Status == TestStatus.Started);
        }

        public Test GetTestByNumber(int testNumber)
        {
            return _dbContext.Set<Test>().FirstOrDefault(t => t.Number == testNumber);
        }

        public TestAnswer GetTestAnswerByUserId(string userId, int testId)
        {
            return _dbContext.Set<TestAnswer>().FirstOrDefault(el => el.UserId == userId && el.TestId == testId);
        }

        public List<Test> GetAllTests()
        {
            return _dbContext.Set<Test>()
                .OrderBy(t => t.Number).ToList();
        }

        public List<Test> GetTestsWithUserAnswers()
        {
            return _dbContext.Set<Test>()
                .Include(t => t.Answers)
                .ThenInclude(ta => ta.User)
                .OrderBy(el => el.Number).ToList();
        }

        public int GetCorrectAnswersCountForUser(string userId)
        {
            return _dbContext.Set<TestAnswer>()
                .Where(a => a.UserId == userId)
                .GroupBy(t => t.TestId)
                .Count();
        }
    }
}