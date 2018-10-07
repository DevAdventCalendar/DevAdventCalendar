using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Services
{
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository _homeRepository;

        public HomeService(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        public Test GetCurrentTest()
        {
            return _homeRepository.GetCurrentTest();
        }

        public TestAnswer GetTestAnswerByUserId(string userId, int testId)
        {
            return _homeRepository.GetTestAnswerByUserId(userId, testId);
        }

        public List<Test> GetTestsWithUserAnswers()
        {
            return _homeRepository.GetTestsWithUserAnswers();
        }
    }
}