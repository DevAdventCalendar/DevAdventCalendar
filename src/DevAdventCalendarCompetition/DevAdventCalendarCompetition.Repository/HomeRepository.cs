using System;
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
        private readonly ApplicationDbContext _dbContext;

        public HomeRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Test GetCurrentTest()
        {
            return this._dbContext.Set<Test>().FirstOrDefault(el => el.Status == TestStatus.Started);
        }

        public Test GetTestByNumber(int testNumber)
        {
            return this._dbContext.Set<Test>().FirstOrDefault(t => t.Number == testNumber);
        }

        public TestAnswer GetTestAnswerByUserId(string userId, int testId)
        {
            return this._dbContext.Set<TestAnswer>().FirstOrDefault(el => el.UserId == userId && el.TestId == testId);
        }

        public List<Test> GetAllTests()
        {
            return this._dbContext.Set<Test>()
                .OrderBy(t => t.Number).ToList();
        }

        public List<Test> GetTestsWithUserAnswers()
        {
            return this._dbContext.Set<Test>()
                .Include(t => t.WrongAnswers)
                .Include(t => t.Answers)
                .ThenInclude(ta => ta.User)
                .OrderBy(el => el.Number).ToList();
        }

        public int GetCorrectAnswersCountForUser(string userId)
        {
            return this._dbContext.Set<TestAnswer>()
                .Where(a => a.UserId == userId)
                .GroupBy(t => t.TestId)
                .Count();
        }

        public List<Result> GetAllTestResults()
        {
            return this._dbContext.Set<Result>()
                .Include(u => u.User)
                .OrderBy(r => r.Id)
                .ToList();
        }

        public int GetUserPosition(string userId)
        {
            var result = this._dbContext.Results
                .FirstOrDefault(x => x.UserId == userId);

            if (result == null)
            {
                return 0;
            }

            if (result.FinalPlace > 0)
            {
                return result.FinalPlace;
            }

            if (result.Week3Place > 0)
            {
                return result.Week3Place;
            }

            if (result.Week2Place > 0)
            {
                return result.Week2Place;
            }

            if (result.Week1Place > 0)
            {
                return result.Week1Place;
            }

            return 0;
        }

        public List<Result> GetTestResultsForDateRange(int weekNumber)
        {
            switch (weekNumber)
            {
                case 1:
                    return this._dbContext.Results
                        .Include(u => u.User)
                        .Where(r => r.Week1Points >= 0)
                        .OrderBy(r => r.Week1Place)
                        .ToList();
                case 2:
                    return this._dbContext.Results
                        .Include(u => u.User)
                        .Where(r => r.Week2Points >= 0)
                        .OrderBy(r => r.Week2Place)
                        .ToList();
                case 3:
                    return this._dbContext.Results
                        .Include(u => u.User)
                        .Where(r => r.Week3Points >= 0)
                        .OrderBy(r => r.Week3Place)
                        .ToList();
                case 4:
                    return this._dbContext.Results
                        .Include(u => u.User)
                        .Where(r => r.FinalPoints >= 0)
                        .OrderBy(r => r.FinalPlace)
                        .ToList();
                default:
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                    throw new ArgumentException("Invalid week number.");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            }
        }
    }
}