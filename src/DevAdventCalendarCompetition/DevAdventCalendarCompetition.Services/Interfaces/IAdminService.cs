using DevAdventCalendarCompetition.Services.Models;
using System;
using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IAdminService
    {
        List<TestDto> GetAllTests();

        TestDto GetTestById(int id);

        TestDto GetPreviousTest(int testNumber);

        void UpdateTestDates(TestDto test, DateTime startTime, DateTime endTime);

        void UpdateTestEndDate(TestDto test, DateTime endTime);

        void ResetTestDates();

        void ResetTestAnswers();
    }
}