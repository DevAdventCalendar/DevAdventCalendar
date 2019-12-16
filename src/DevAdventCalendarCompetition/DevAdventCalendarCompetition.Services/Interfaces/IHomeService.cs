using System.Collections.Generic;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IHomeService
    {
        TestDto GetCurrentTest();

        TestAnswerDto GetTestAnswerByUserId(string userId, int testId);

        List<TestDto> GetCurrentTests();

        List<TestWithAnswerListDto> GetTestsWithUserAnswers();

        string CheckTestStatus(int testNumber);

        int GetCorrectAnswersCountForUser(string userId);

        Dictionary<int, List<TestResultDto>> GetAllTestResults();

        int[] GetUserPositions(string userId);

        string PrepareUserEmailForRODO(string email);
    }
}