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

                TestResultService service = new TestResultService(repository,
                    correctAnswersPointsRule,
                    bonusPointsRule,
                    placeRule);

                var numberOfWeek = args.FirstOrDefault();
                if (numberOfWeek == null)
                {

                    DateTime startDateTime = new DateTime(DateTime.Today.Year, 12, 1, 20, 0, 0);
                    Trace.WriteLine($"Start date {startDateTime}");
                    DateTime endDateTime = new DateTime(DateTime.Today.Year, 12, 24, 23, 59, 59);
                    Trace.WriteLine($"End date {endDateTime}");

                    Trace.WriteLine($"Starting calculating points...");
                    service.CalculateResults(startDateTime, endDateTime);
                    Trace.WriteLine($"Finished calculating points");
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
