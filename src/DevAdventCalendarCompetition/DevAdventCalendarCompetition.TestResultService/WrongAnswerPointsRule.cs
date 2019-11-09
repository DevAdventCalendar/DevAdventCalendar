using System;
using System.Collections.Generic;
using System.Text;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.TestResultService
{
    public class WrongAnswerPointsRule : ITestResultPointsRule<TestWrongAnswer>
    {
        public int CalculatePoints(List<TestWrongAnswer> userAnswers)
        {
            throw new NotImplementedException();
        }
    }
}
