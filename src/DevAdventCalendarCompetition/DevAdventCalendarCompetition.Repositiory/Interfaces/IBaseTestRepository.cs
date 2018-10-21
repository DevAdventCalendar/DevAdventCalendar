using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.Repository.Interfaces
{
    public interface IBaseTestRepository
    {
        Test GetByNumber(int testNumber);

        void AddAnswer(TestAnswer testAnswer);

        TestAnswer GetAnswerByTestId(int testId);
    }
}