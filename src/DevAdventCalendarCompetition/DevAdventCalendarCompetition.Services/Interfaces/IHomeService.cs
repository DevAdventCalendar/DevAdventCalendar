using DevAdventCalendarCompetition.Services.Models;
using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IHomeService
    {
        TestDto GetCurrentTest();

        TestAnswerDto GetTestAnswerByUserId(string userId, int testId);

        List<TestWithAnswerListDto> GetTestsWithUserAnswers();
    }
}