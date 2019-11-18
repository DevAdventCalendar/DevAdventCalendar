using DevAdventCalendarCompetition.TestResultService.Interfaces;

namespace DevAdventCalendarCompetition.TestResultService
{
    public class CorrectAnswerPointsRule : ITestResultPointsRule
    {
        public int CalculatePoints(int answersCount)
        {
            return answersCount * 100;
        }
    }
}
