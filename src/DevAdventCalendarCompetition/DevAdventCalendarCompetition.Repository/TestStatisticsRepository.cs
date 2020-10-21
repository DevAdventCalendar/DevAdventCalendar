using System;
using System.Collections.Generic;
using System.Linq;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace DevAdventCalendarCompetition.Repository
{
    public class TestStatisticsRepository : ITestStatisticsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TestStatisticsRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        /*  [in #1] ID of user that wants to know count of wrong answers
         *  [in #2] ID of test that user wants to know count of wrong answers
         *
         *  [out] Count of submitted wrong answers
         */
        public int GetUserTestWrongAnswerCount(string userID, int testID) // get count of wrong answers
        {
            return this._dbContext
                .UserTestWrongAnswers
                .Where(a => a.TestId == testID && a.UserId == userID)
                .Count();
        }

        /*  [in]#1 ID of user that wants to know when he submitted correct answer
         *  [in]#2 ID of test that user wants to know when he submitted correct answer
         *
         *  [out] Date of submission of corect answer
         */
        public DateTime GetUserTestCorrectAnswerDate(string userID, int testID) // get DateTime of correct answer
        {
            return this._dbContext
                .UserTestCorrectAnswers
                .Where(a => a.TestId == testID && a.UserId == userID)
                .Select(a => a.AnsweringTime)
                .Single(); // I assume there is only one row
        }
    }
}
