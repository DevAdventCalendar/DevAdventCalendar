using DevAdventCalendarCompetition.Repository.Models;
using System;
using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IAdminService
    {
        List<Test> GetAllTests();

        Test GetTestById(int id);

        Test GetPreviousTest(int testNumber);

        void UpdateTestDates(Test test, DateTime startTime, DateTime endTime);

        void UpdateTestEndDate(Test test, DateTime endTime);

        void ResetTestDates();

        void ResetTestAnswers();
    }
}