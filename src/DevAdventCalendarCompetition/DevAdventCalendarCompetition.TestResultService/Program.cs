using DevAdventCalendarCompetition.Services.Options;
using DevAdventCalendarCompetition.TestResultService.Interfaces;
using System;
using System.Diagnostics;
using System.Linq;

namespace DevAdventCalendarCompetition.TestResultService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new TestResultDbContextFactory().CreateDbContext())
            {
                ITestResultRepository repository = new TestResultRepository(context);
                ITestResultPointsRule correctAnswersPointsRule = new CorrectAnswerPointsRule();
                ITestResultPointsRule bonusPointsRule = new BonusPointsRule();
                ITestResultPlaceRule placeRule = new AnsweringTimePlaceRule();
                TestSettings testSettings = new TestSettings();

                TestResultService service = new TestResultService(repository,
                    correctAnswersPointsRule,
                    bonusPointsRule,
                    placeRule,
                    testSettings);

                var numberOfWeek = args.FirstOrDefault();
                if (numberOfWeek == null)
                {
                    Trace.WriteLine($"Starting calculating final points...");
                    service.CalculateFinalResults();
                    Trace.WriteLine($"Finished calculating final points");
                }
                else
                {
                    var isWeekNumberValid = int.TryParse(numberOfWeek, out int numberOfWeekInt);
                    if(isWeekNumberValid)
                    {
                        if (numberOfWeekInt > 0 && numberOfWeekInt < 4)
                        {
                            Trace.WriteLine($"Starting calculating points for week {numberOfWeek}...");
                            service.CalculateWeeklyResults(numberOfWeekInt);
                            Trace.WriteLine($"Finished calculating points for week {numberOfWeek}");
                        }
                        else
                        {
                            throw new Exception("Parametr numeru tygodnia przyjmuje wartość od 1 do 3");
                        }
                    }
                    else
                    {
                        throw new Exception("Podaj prawidłową wartość numeru tygodnia");
                    }                    
                }                
            }
        }
    }
}
