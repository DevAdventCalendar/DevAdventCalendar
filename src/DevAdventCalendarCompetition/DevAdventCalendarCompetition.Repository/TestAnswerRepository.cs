using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.Repository
{
    public class TestAnswerRepository : ITestAnswerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TestAnswerRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
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

        public TestAnswer GetTestAnswerByUserId(string userId, int testId)
        {
            return this._dbContext.Set<TestAnswer>().FirstOrDefault(el => el.UserId == userId && el.TestId == testId);
        }

        public bool HasUserAnsweredTest(string userId, int testId)
        {
            return this._dbContext.Set<TestAnswer>().FirstOrDefault(el => el.TestId == testId && el.UserId == userId) != null;
        }

        public void DeleteTestAnswers()
        {
            var testAnswers = this._dbContext.Set<TestAnswer>().ToList();
            this._dbContext.Set<TestAnswer>().RemoveRange(testAnswers);
            this._dbContext.SaveChanges();
        }

        public int GetCorrectAnswersCountForUser(string userId)
        {
            return this._dbContext.Set<TestAnswer>()
                .Where(a => a.UserId == userId)
                .AsEnumerable()
                .GroupBy(t => t.TestId)
                .Count();
        }

        public IDictionary<string, int> GetCorrectAnswersPerUserForDateRange(DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            return this._dbContext
                .TestAnswer
                .Where(a => a.AnsweringTime.CompareTo(dateFrom.DateTime) >= 0 &&
                            a.AnsweringTime.CompareTo(dateTo.DateTime) < 0)
                .AsEnumerable()
                .GroupBy(a => a.UserId)
                .Select(ug => new KeyValuePair<string, int>(ug.Key, ug.Count()))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public IDictionary<string, int> GetWrongAnswersPerUserForDateRange(DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            return this._dbContext
                .TestWrongAnswer
                .Where(a => a.Time.CompareTo(dateFrom.DateTime) >= 0 &&
                            a.Time.CompareTo(dateTo.DateTime) < 0)
                .AsEnumerable()
                .GroupBy(a => a.UserId)
                .Select(ug => new KeyValuePair<string, int>(ug.Key, ug.Count()))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}
