using System.Collections.Generic;
using System.Linq;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<Test> GetAll()
        {
            return this._dbContext.Set<Test>().OrderBy(t => t.StartDate).ToList();
        }

        public Test GetById(int testId)
        {
            return this._dbContext.Set<Test>().FirstOrDefault(el => el.Id == testId);
        }

        public void UpdateDates(Test test)
        {
            if (test is null)
            {
                throw new System.ArgumentNullException(nameof(test));
            }

            var dbTest = this._dbContext.Set<Test>().FirstOrDefault(el => el.Id == test.Id);
            if (dbTest != null)
            {
                dbTest.StartDate = test.StartDate;
                dbTest.EndDate = test.EndDate;
                this._dbContext.SaveChanges();
            }
        }

        public void UpdateEndDate(Test test)
        {
            var dbTest = this._dbContext.Set<Test>().FirstOrDefault(el => el.Id == test.Id);
            if (dbTest != null)
            {
#pragma warning disable CA1062 // Validate arguments of public methods
                dbTest.EndDate = test.EndDate;
#pragma warning restore CA1062 // Validate arguments of public methods
                this._dbContext.SaveChanges();
            }
        }

        public void ResetTestDates()
        {
            var tests = this._dbContext.Set<Test>().ToList();
            tests.ForEach(el =>
            {
                el.StartDate = null;
                el.EndDate = null;
            });
            this._dbContext.SaveChanges();
        }

        public void DeleteTestAnswers()
        {
            var testAnswers = this._dbContext.Set<TestAnswer>().ToList();
            this._dbContext.Set<TestAnswer>().RemoveRange(testAnswers);
            this._dbContext.SaveChanges();
        }
    }
}