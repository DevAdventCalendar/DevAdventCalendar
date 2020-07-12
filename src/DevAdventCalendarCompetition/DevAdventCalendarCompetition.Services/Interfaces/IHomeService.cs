using System.Collections.Generic;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IHomeService
    {
        UserTestCorrectAnswerDto GetCorrectAnswerByUserId(string userId, int testId);

        List<TestDto> GetCurrentTests();

        List<TestWithUserCorrectAnswerListDto> GetTestsWithUserAnswers();

        string CheckTestStatus(int testNumber);

        int GetCorrectAnswersCountForUser(string userId);
    }
}