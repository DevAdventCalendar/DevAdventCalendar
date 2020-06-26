using System;
using System.Collections.Generic;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.Repository.Interfaces
{
    public interface ITestRepository
    {
        List<Test> GetAll();

        Test GetById(int testId);

        Test GetByNumber(int testNumber);

        Test GetCurrentTest();

        List<Test> GetTestsWithUserAnswers();

        void AddTest(Test test);

        void UpdateDates(int testId, DateTime startDate, DateTime endDate);

        void UpdateEndDate(int testId, DateTime endDate);

        void ResetTestDates();
    }
}