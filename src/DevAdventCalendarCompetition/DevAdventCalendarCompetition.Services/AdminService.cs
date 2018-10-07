using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IBaseTestRepository _baseTestRepository;

        public AdminService(IAdminRepository adminRepository, IBaseTestRepository baseTestRepository)
        {
            _adminRepository = adminRepository;
            _baseTestRepository = baseTestRepository;
        }

        public List<Test> GetAllTests()
        {
            return _adminRepository.GetAll();
        }

        public Test GetTestById(int testId)
        {
            return _adminRepository.GetById(testId);
        }

        public Test GetPreviousTest(int testNumber)
        {
            return _baseTestRepository.GetByNumber(testNumber);
        }

        public void UpdateTestDates(Test test, DateTime startTime, DateTime endTime)
        {
            test.StartDate = startTime;
            test.EndDate = endTime;
            _adminRepository.UpdateDates(test);
        }

        public void UpdateTestEndDate(Test test, DateTime endTime)
        {
            test.EndDate = endTime;
            _adminRepository.UpdateEndDate(test);
        }

        public void ResetTestDates()
        {
            _adminRepository.ResetTestDates();
        }

        public void ResetTestAnswers()
        {
            _adminRepository.DeleteTestAnswers();
        }
    }
}