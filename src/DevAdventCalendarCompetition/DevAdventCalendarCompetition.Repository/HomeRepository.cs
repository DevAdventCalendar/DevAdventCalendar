using System.Collections.Generic;
using System.Linq;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DevAdventCalendarCompetition.Repository
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public HomeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Test GetCurrentTest()
        {
            return this.dbContext.Set<Test>().FirstOrDefault(el => el.Status == TestStatus.Started);
        }

        public Test GetTestByNumber(int testNumber)
        {
            return this.dbContext.Set<Test>().FirstOrDefault(t => t.Number == testNumber);
        }

        public TestAnswer GetTestAnswerByUserId(string userId, int testId)
        {
            return this.dbContext.Set<TestAnswer>().FirstOrDefault(el => el.UserId == userId && el.TestId == testId);
        }

        public List<Test> GetAllTests()
        {
            return this.dbContext.Set<Test>()
                .OrderBy(t => t.Number).ToList();
        }

        public List<Test> GetTestsWithUserAnswers()
        {
            return this.dbContext.Set<Test>()
                .Include(t => t.WrongAnswers)
                .Include(t => t.Answers)
                .ThenInclude(ta => ta.User)
                .OrderBy(el => el.Number).ToList();
        }

        public int GetCorrectAnswersCountForUser(string userId)
        {
            return this.dbContext.Set<TestAnswer>()
                .Where(a => a.UserId == userId)
                .GroupBy(t => t.TestId)
                .Count();
        }

        public List<Result> GetAllTestResults()
        {
            return this.dbContext.Set<Result>()
                .Include(u => u.User)
                .OrderBy(r => r.Id)
                .ToList();
        }

        public int GetUserPosition(string userId)
        {
            var result = this.dbContext.Results
                .FirstOrDefault(x => x.UserId == userId);

            if (result == null)
            {
                return 0;
            }

            var indexOfResult = this.dbContext.Results
                .OrderByDescending(r => r.Points)
                .IndexOf(result);

            return ++indexOfResult;
        }
    }
}