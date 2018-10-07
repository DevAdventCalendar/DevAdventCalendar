using DevAdventCalendarCompetition.Repository.Models;
using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IHomeService
    {
        Test GetCurrentTest();

        TestAnswer GetTestAnswerByUserId(string userId, int testId);

        List<Test> GetTestsWithUserAnswers();
    }
}