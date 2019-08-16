using System.Linq;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.Repository
{
    public class BaseTestRepository : IBaseTestRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BaseTestRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Test GetByNumber(int testNumber)
        {
            return this.dbContext.Set<Test>().FirstOrDefault(el => el.Number == testNumber);
        }

        public void AddTest(Test test)
        {
            this.dbContext.Set<Test>().Add(test);
            this.dbContext.SaveChanges();
        }

        public void AddAnswer(TestAnswer testAnswer)
        {
            this.dbContext.Set<TestAnswer>().Add(testAnswer);
            this.dbContext.SaveChanges();
        }

        public void AddWrongAnswer(TestWrongAnswer wrongAnswer)
        {
            this.dbContext.Set<TestWrongAnswer>().Add(wrongAnswer);
            this.dbContext.SaveChanges();
        }

        public void UpdateAnswer(TestAnswer testAnswer)
        {
            this.dbContext.Set<TestAnswer>().Update(testAnswer);
            this.dbContext.SaveChanges();
        }

        public TestAnswer GetAnswerByTestId(int testId)
        {
            return this.dbContext.Set<TestAnswer>().FirstOrDefault(el => el.TestId == testId);
        }

        public bool HasUserAnsweredTest(string userId, int testId)
        {
            return this.dbContext.Set<TestAnswer>().FirstOrDefault(el => el.TestId == testId && el.UserId == userId) != null;
        }
    }
}