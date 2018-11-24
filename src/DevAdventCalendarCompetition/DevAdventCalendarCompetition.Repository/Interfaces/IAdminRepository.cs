using DevAdventCalendarCompetition.Repository.Models;
using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Repository.Interfaces
{
    public interface IAdminRepository
    {
        List<Test> GetAll();

        Test GetById(int testId);

        void UpdateDates(Test test);

        void UpdateEndDate(Test test);

        void ResetTestDates();

        void DeleteTestAnswers();
    }
}