using System;
using System.Collections.Generic;
using System.Text;

namespace DevAdventCalendarCompetition.TestResultService
{
    
    class TestResultService : ITestResultPlaceRule, ITestResultPointsRule, ITestResultRepository
    {
        CalculateResults(dateFrom: DateTimeOffset : dateTo: DateTimeOffset);

        CalculateWeeklyResults(WeekNumber int);

        SaveFinalResults();

        
    }

 
}