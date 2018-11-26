using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using System.Collections.Generic;
using System.Linq;

namespace DevAdventCalendarCompetition.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Test> GetAll()
        {
            return _dbContext.Set<Test>().OrderBy(t => t.StartDate).ToList();
        }

        public Test GetById(int testId)
        {
            return _dbContext.Set<Test>().FirstOrDefault(el => el.Id == testId);
        }

        public void UpdateDates(Test test)
        {
            var dbTest = _dbContext.Set<Test>().FirstOrDefault(el => el.Id == test.Id);
            if (dbTest != null)
            {
                dbTest.StartDate = test.StartDate;
                dbTest.EndDate = test.EndDate;
                _dbContext.SaveChanges();
            }
        }

        public void UpdateEndDate(Test test)
        {
            var dbTest = _dbContext.Set<Test>().FirstOrDefault(el => el.Id == test.Id);
            if (dbTest != null)
            {
                dbTest.EndDate = test.EndDate;
                _dbContext.SaveChanges();
            }
        }

        public void ResetTestDates()
        {
            var tests = _dbContext.Set<Test>().ToList();
            tests.ForEach(el =>
            {
                el.StartDate = null;
                el.EndDate = null;
            });
            _dbContext.SaveChanges();
        }

        public void DeleteTestAnswers()
        {
            var testAnswers = _dbContext.Set<TestAnswer>().ToList();
            _dbContext.Set<TestAnswer>().RemoveRange(testAnswers);
            _dbContext.SaveChanges();
        }
    }
}