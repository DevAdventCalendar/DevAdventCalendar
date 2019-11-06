using System;
using System.Collections.Generic;
using System.Text;

namespace DevAdventCalendarCompetition.TestResultService
{
    public class WrongAnswerPointsRule : ITestResultPointsRule
    {
        public void Calculate()
        {
            throw new NotImplementedException();
        }
    }
}
