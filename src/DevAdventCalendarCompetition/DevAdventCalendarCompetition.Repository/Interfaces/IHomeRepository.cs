using DevAdventCalendarCompetition.Repository.Models;
using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Repository.Interfaces
{
    public interface IHomeRepository
    {
        Test GetCurrentTest();

        Test GetTestByNumber(int testNumber);

        TestAnswer GetTestAnswerByUserId(string userId, int testId);

        List<Test> GetAllTests();
        
        List<Test> GetTestsWithUserAnswers();

        int GetCorrectAnswersCountForUser(string userId);

        List<Result> GetAllTestResults();
    }
}