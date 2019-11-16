using System.Linq;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.Repository
{
    public class BaseTestRepository : IBaseTestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseTestRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Test GetByNumber(int testNumber)
        {
            return this._dbContext.Set<Test>().FirstOrDefault(el => el.Number == testNumber);
        }

        public void AddTest(Test test)
        {
            this._dbContext.Set<Test>().Add(test);
            this._dbContext.SaveChanges();
        }

        public void AddAnswer(TestAnswer testAnswer)
        {
            this._dbContext.Set<TestAnswer>().Add(testAnswer);
            this._dbContext.SaveChanges();
        }

        public void AddWrongAnswer(TestWrongAnswer wrongAnswer)
        {
            this._dbContext.Set<TestWrongAnswer>().Add(wrongAnswer);
            this._dbContext.SaveChanges();
        }

        public void UpdateAnswer(TestAnswer testAnswer)
        {
            this._dbContext.Set<TestAnswer>().Update(testAnswer);
            this._dbContext.SaveChanges();
        }

        public TestAnswer GetAnswerByTestId(int testId)
        {
            return this._dbContext.Set<TestAnswer>().FirstOrDefault(el => el.TestId == testId);
        }

        public bool HasUserAnsweredTest(string userId, int testId)
        {
            return this._dbContext.Set<TestAnswer>().FirstOrDefault(el => el.TestId == testId && el.UserId == userId) != null;
        }
    }
}