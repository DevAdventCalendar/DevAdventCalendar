using System;
using System.Collections.Generic;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.Repository.Interfaces
{
    public interface ITestRepository
    {
        List<Test> GetAllTests();

        Test GetTestById(int testId);

        Test GetTestByNumber(int testNumber);

        Test GetCurrentTest();

        List<Test> GetTestsWithUserAnswers();

        void AddTest(Test test);

        void UpdateTestDates(int testId, DateTime startDate, DateTime endDate);

        void UpdateTestEndDate(int testId, DateTime endDate);
    }
}