using AutoMapper;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using System;

namespace DevAdventCalendarCompetition.Services
{
    public class BaseTestService : IBaseTestService
    {
        private readonly IBaseTestRepository _baseTestRepository;
		private readonly IMapper _mapper;

        public BaseTestService(
			IBaseTestRepository baseTestRepository,
			IMapper mapper)
        {
            _baseTestRepository = baseTestRepository;
			_mapper = mapper;
        }

        public TestDto GetTestByNumber(int testNumber)
        {
            var test = _baseTestRepository.GetByNumber(testNumber);
            var testDto = _mapper.Map<TestDto>(test);
            return testDto;
        }

        public void AddTestAnswer(int testId, string userId, DateTime testStartDate)
        {
            var testAnswer = new TestAnswer()
            {
                TestId = testId,
                UserId = userId,
                AnsweringTime = DateTime.Now,
                AnsweringTimeOffset = DateTime.Now.Subtract(testStartDate)
            };

            _baseTestRepository.AddAnswer(testAnswer);
        }

        public TestAnswerDto GetAnswerByTestId(int testId)
        {
            var testAnswer = _baseTestRepository.GetAnswerByTestId(testId);
            var testAnswerDto = _mapper.Map<TestAnswerDto>(testAnswer);
            return testAnswerDto;
        }
    }
}