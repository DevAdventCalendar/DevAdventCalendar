using System.Collections.Generic;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.Repository.Interfaces
{
    public interface ITestRepository
    {
        Test GetById(int testId);

        Test GetByNumber(int testNumber);

        Test GetCurrentTest();

        List<Test> GetAll();

        List<Test> GetTestsWithUserAnswers();

        void AddTest(Test test);
    }
}