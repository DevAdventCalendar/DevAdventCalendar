using System;
using System.Collections.Generic;
using System.Text;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.TestResultService
{
    public interface ITestResultPointsRule
    {
       int CalculatePoints(List<TestAnswer> userAnswers);
    }
}
                   