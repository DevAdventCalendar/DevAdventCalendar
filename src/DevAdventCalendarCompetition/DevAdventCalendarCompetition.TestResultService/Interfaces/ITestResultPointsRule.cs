using System;
using System.Collections.Generic;
using System.Text;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.TestResultService
{
    public interface ITestResultPointsRule<T>
    {
       int CalculatePoints(List<T> userAnswers);
    }
}
                   