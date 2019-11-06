using System;
using System.Collections.Generic;
using System.Text;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.TestResultService
{
    public class ICorrectAnswerPointsRule : ITestResultPointsRule
    {
        public int CalculatePoints(List<TestAnswer> userAnswers)
        {
            throw new NotImplementedException();
        }
    }
}
