using System;
using System.Collections.Generic;
using System.Linq;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace DevAdventCalendarCompetition.Repository
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StatisticsRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        /*  [in #1] ID of user that wants to know count of wrong answers
         *  [in #2] ID of test that user wants to know count of wrong answers
         *
         *  [out] Count of submitted wrong answers
         */
        public int GetUserTestWrongAnswerCount(string userId, int testId)
        {
            return this._dbContext
                .UserTestWrongAnswers
                .Count(a => a.TestId == testId && a.UserId == userId);
        }

        /*  [in]#1 ID of user that wants to know when he submitted correct answer
         *  [in]#2 ID of test that user wants to know when he submitted correct answer
         *
         *  [out] Date of submission of corect answer
         */
        public DateTime? GetUserTestCorrectAnswerDate(string userId, int testId)
        {
            var dbTest = this._dbContext.Set<Test>().FirstOrDefault(el => el.Id == testId);
            if (dbTest != null)
            {
                return this._dbContext
                .UserTestCorrectAnswers
                .Where(a => a.TestId == testId && a.UserId == userId)
                .Select(a => a.AnsweringTime)
                .SingleOrDefault();
            }

            return null;
        }
    }
}
