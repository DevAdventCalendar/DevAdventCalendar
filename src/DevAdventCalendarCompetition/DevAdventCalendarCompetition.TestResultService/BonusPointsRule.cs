using DevAdventCalendarCompetition.TestResultService.Interfaces;

namespace DevAdventCalendarCompetition.TestResultService
{
    public class BonusPointsRule : ITestResultPointsRule
    {
        private const int MaxBonus = 30;

        public int CalculatePoints(int answersCount)
        {
            return MaxBonus - answersCount * 5;
        }
    }
}
