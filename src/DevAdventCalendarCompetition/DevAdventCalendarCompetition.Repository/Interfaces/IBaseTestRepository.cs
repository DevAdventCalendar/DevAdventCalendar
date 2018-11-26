using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.Repository.Interfaces
{
    public interface IBaseTestRepository
    {
        Test GetByNumber(int testNumber);

        TestAnswer GetAnswerByTestId(int testId);

        void AddTest(Test test);

        void AddAnswer(TestAnswer testAnswer);
        void UpdateAnswer(TestAnswer testAnswer);
		void AddWrongAnswer(TestWrongAnswer wrongAnswer);
	}
}