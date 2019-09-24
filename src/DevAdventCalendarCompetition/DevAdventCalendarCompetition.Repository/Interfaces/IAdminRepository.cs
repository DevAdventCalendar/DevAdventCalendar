using System.Collections.Generic;
using DevAdventCalendarCompetition.Repository.Models;

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