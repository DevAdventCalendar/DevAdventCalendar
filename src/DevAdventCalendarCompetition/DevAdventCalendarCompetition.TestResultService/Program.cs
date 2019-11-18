using System;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.TestResultService.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

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

                service.CalculateWeeklyResults(1);
            }
        }
    }
}
