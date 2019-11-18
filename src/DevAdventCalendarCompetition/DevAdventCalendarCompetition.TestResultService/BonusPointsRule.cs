using DevAdventCalendarCompetition.TestResultService.Interfaces;

namespace DevAdventCalendarCompetition.TestResultService
{
    public class BonusPointsRule : ITestResultPointsRule
    {
        public int CalculatePoints(int answersCount)
        {
            return 30 - answersCount * 5;
        }
    }
}
