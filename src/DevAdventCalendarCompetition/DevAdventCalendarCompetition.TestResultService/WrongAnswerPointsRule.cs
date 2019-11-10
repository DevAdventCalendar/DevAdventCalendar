using System;
using System.Collections.Generic;
using System.Text;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.TestResultService.Interfaces;

namespace DevAdventCalendarCompetition.TestResultService
{
    public class WrongAnswerPointsRule : ITestResultPointsRule
    {
        public int CalculatePoints(int answersCount)
        {
            throw new NotImplementedException();
        }
    }
}
