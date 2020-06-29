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

        void UpdateTestDates(int testId, string minutesString);

        void UpdateTestEndDate(int testId, DateTime endTime);
    }
}