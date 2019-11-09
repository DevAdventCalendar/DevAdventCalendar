using System;
using System.Collections.Generic;
using System.Text;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.TestResultService
{
    public class CorrectAnswerPointsRule : ITestResultPointsRule
    {
        public int CalculatePoints(List<TestAnswer> userAnswers)
        {
            return userAnswers.Count * 100;
        }
    }
}
