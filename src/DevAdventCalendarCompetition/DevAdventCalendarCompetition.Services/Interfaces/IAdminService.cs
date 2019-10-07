using System;
using System.Collections.Generic;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IAdminService
    {
        List<TestDto> GetAllTests();

        TestDto GetTestById(int id);

        TestDto GetPreviousTest(int testNumber);

        void AddTest(TestDto testDto);

        void UpdateTestDates(TestDto test, string minutesString);

        void UpdateTestEndDate(TestDto test, DateTime endTime);

        void ResetTestDates();

        void ResetTestAnswers();
    }
}