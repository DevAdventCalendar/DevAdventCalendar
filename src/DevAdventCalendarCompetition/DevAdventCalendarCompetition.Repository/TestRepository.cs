using System;
using System.Collections.Generic;
using System.Linq;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace DevAdventCalendarCompetition.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TestRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<Test> GetAllTests()
        {
            return this._dbContext.Set<Test>().OrderBy(t => t.StartDate).ToList();
        }

        public Test GetTestById(int testId)
        {
            return this._dbContext.Set<Test>().FirstOrDefault(el => el.Id == testId);
        }

        public Test GetTestByNumber(int testNumber)
        {
            return this._dbContext.Set<Test>().FirstOrDefault(el => el.Number == testNumber);
        }

        public void AddTest(Test test)
        {
            this._dbContext.Set<Test>().Add(test);
            this._dbContext.SaveChanges();
        }

        public Test GetCurrentTest()
        {
            return this._dbContext.Set<Test>().FirstOrDefault(el => el.Status == TestStatus.Started);
        }

        public List<Test> GetTestsWithUserAnswers()
        {
            return this._dbContext.Set<Test>()
                .Include(t => t.WrongAnswers)
                .Include(t => t.Answers)
                .ThenInclude(ta => ta.User)
                .OrderBy(el => el.Number).ToList();
        }

        public void UpdateTestDates(int testId, DateTime startDate, DateTime endDate)
        {
            var dbTest = this._dbContext.Set<Test>().FirstOrDefault(el => el.Id == testId);
            if (dbTest != null)
            {
                dbTest.StartDate = startDate;
                dbTest.EndDate = endDate;
                this._dbContext.SaveChanges();
            }
        }

        public void UpdateTestEndDate(int testId, DateTime endDate)
        {
            var dbTest = this._dbContext.Set<Test>().FirstOrDefault(el => el.Id == testId);

            if (dbTest != null)
            {
                dbTest.EndDate = endDate;
                this._dbContext.SaveChanges();
            }
        }
    }
}