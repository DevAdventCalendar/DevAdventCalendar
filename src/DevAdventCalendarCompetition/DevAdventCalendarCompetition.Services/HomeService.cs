using AutoMapper;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
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

        public TestDto GetCurrentTest()
        {
            var test = _homeRepository.GetCurrentTest();
            var testDto = Mapper.Map<TestDto>(test);
            return testDto;
        }

        public TestAnswerDto GetTestAnswerByUserId(string userId, int testId)
        {
            var testAnswer = _homeRepository.GetTestAnswerByUserId(userId, testId);
            var testAnswerDto = Mapper.Map<TestAnswerDto>(testAnswer);
            return testAnswerDto;
        }

        public List<TestWithAnswerListDto> GetTestsWithUserAnswers()
        {
            var testList = _homeRepository.GetTestsWithUserAnswers();
            var testWithAnswersDtoList = Mapper.Map<List<TestWithAnswerListDto>>(testList);
            return testWithAnswersDtoList;
        }
    }
}