using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevAdventCalendarCompetition.TestResultService
{
    
    public class TestResultService
    {
        private ITestResultRepository _testResultRepository;

        public TestResultService()
        {
            this._testResultRepository = new TestResultRepository();
        }

        void CalculateResults(DateTimeOffset dateFrom, DateTimeOffset dateTo) { }

        void CalculateWeeklyResults(int WeekNumber) { }

        void SaveFinalResults()
        {
            throw new NotImplementedException();
        }
    }

 
}