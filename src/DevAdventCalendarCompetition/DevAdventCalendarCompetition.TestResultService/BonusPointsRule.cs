using DevAdventCalendarCompetition.TestResultService.Interfaces;

namespace DevAdventCalendarCompetition.TestResultService
{
    public class BonusPointsRule : ITestResultPointsRule
    {
        private const int MaxBonus = 30;

        public int CalculatePoints(int answersCount)
        {
            return answersCount > 6 ? 0 : MaxBonus - answersCount * 5;
        }
    }
}
