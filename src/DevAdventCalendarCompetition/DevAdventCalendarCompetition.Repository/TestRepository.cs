using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public List<Test> GetAll()
        {
            return this._dbContext.Set<Test>().OrderBy(t => t.StartDate).ToList();
        }

        /*
        // Duplicated method from Home 'repository'
        // public List<Test> GetAllTests()
        // {
        //    return this._dbContext.Set<Test>()
        //        .OrderBy(t => t.Number).ToList();
        // }
        */

        public Test GetById(int testId)
        {
            return this._dbContext.Set<Test>().FirstOrDefault(el => el.Id == testId);
        }

        public void AddTest(Test test)
        {
            this._dbContext.Set<Test>().Add(test);
        }

        public Test GetByNumber(int testNumber)
        {
            return this._dbContext.Set<Test>().FirstOrDefault(el => el.Number == testNumber);
        }

        /*
        // Duplicated method from Home 'repository'
        // public Test GetTestByNumber(int testNumber)
        // {
        //    return this._dbContext.Set<Test>().FirstOrDefault(t => t.Number == testNumber);
        // }
        */

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
    }
}