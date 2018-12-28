using DevAdventCalendarCompetition.Services.Models;
using System.Collections.Generic;

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

        List<TestResultDto> GetAllTestResults();

        int GetUserPosition(string userId);
    }
}