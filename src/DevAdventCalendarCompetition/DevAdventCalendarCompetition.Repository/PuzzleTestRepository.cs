using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.Repository
{
    public class PuzzleTestRepository : IPuzzleTestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PuzzleTestRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TestAnswer GetEmptyAnswerForStartedTestByUser(string userId)
        {
            return _dbContext.TestAnswer.FirstOrDefault(a =>
                a.AnsweringTime == DateTime.MinValue &&
                //a.Answer == null && TODO: Correct checking for null answer
                a.UserId == userId);
        }
    }
}