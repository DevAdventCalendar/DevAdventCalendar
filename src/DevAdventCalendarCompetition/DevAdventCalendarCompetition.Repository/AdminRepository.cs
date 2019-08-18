using System.Collections.Generic;
using System.Linq;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext dbContext;

        public AdminRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Test> GetAll()
        {
            return this.dbContext.Set<Test>().OrderBy(t => t.StartDate).ToList();
        }

        public Test GetById(int testId)
        {
            return this.dbContext.Set<Test>().FirstOrDefault(el => el.Id == testId);
        }

        public void UpdateDates(Test test)
        {
            var dbTest = this.dbContext.Set<Test>().FirstOrDefault(el => el.Id == test.Id);
            if (dbTest != null)
            {
#pragma warning disable CA1062 // Validate arguments of public methods
                dbTest.StartDate = test.StartDate;
#pragma warning restore CA1062 // Validate arguments of public methods
                dbTest.EndDate = test.EndDate;
                this.dbContext.SaveChanges();
            }
        }

        public void UpdateEndDate(Test test)
        {
            var dbTest = this.dbContext.Set<Test>().FirstOrDefault(el => el.Id == test.Id);
            if (dbTest != null)
            {
#pragma warning disable CA1062 // Validate arguments of public methods
                dbTest.EndDate = test.EndDate;
#pragma warning restore CA1062 // Validate arguments of public methods
                this.dbContext.SaveChanges();
            }
        }

        public void ResetTestDates()
        {
            var tests = this.dbContext.Set<Test>().ToList();
            tests.ForEach(el =>
            {
                el.StartDate = null;
                el.EndDate = null;
            });
            this.dbContext.SaveChanges();
        }

        public void DeleteTestAnswers()
        {
            var testAnswers = this.dbContext.Set<TestAnswer>().ToList();
            this.dbContext.Set<TestAnswer>().RemoveRange(testAnswers);
            this.dbContext.SaveChanges();
        }
    }
}