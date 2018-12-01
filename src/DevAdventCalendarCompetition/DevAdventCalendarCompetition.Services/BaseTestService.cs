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
        private readonly StringHasher _stringHasher;

        public BaseTestService(
            IBaseTestRepository baseTestRepository,
            IMapper mapper,
            StringHasher stringHasher)
        {
            _baseTestRepository = baseTestRepository;
            _mapper = mapper;
            _stringHasher = stringHasher;
        }

        public TestDto GetTestByNumber(int testNumber)
        {
            var test = _baseTestRepository.GetByNumber(testNumber);

            var testDto = _mapper.Map<TestDto>(test);
            return testDto;
        }

        public void AddTestAnswer(int testId, string userId, DateTime testStartDate)
        {
            var currentTime = DateTime.Now;
            var answerTimeOffset = currentTime.Subtract(testStartDate);
            var maxAnswerTime = new TimeSpan(0, 23, 59, 59, 999);

            var testAnswer = new TestAnswer()
            {
                TestId = testId,
                UserId = userId,
                AnsweringTime = currentTime,
                AnsweringTimeOffset = answerTimeOffset > maxAnswerTime ? maxAnswerTime : answerTimeOffset
            };

            //TODO remove (for tests only)

            _baseTestRepository.AddAnswer(testAnswer);
        }

        public TestAnswerDto GetAnswerByTestId(int testId)
        {
            var testAnswer = _baseTestRepository.GetAnswerByTestId(testId);
            var testAnswerDto = _mapper.Map<TestAnswerDto>(testAnswer);
            return testAnswerDto;
        }

        public bool HasUserAnsweredTest(string userId, int testId)
        {
            return _baseTestRepository.HasUserAnsweredTest(userId, testId);
        }

        public void AddTestWrongAnswer(string userId, int testId, string wrongAnswer, DateTime wrongAnswerDate)
        {
            var testWrongAnswer = new TestWrongAnswer()
            {
                UserId = userId,
                Time = wrongAnswerDate,
                Answer = wrongAnswer,
                TestId = testId
            };

            _baseTestRepository.AddWrongAnswer(testWrongAnswer);
        }

        public bool VerifyTestAnswer(string userAnswer, string correntAnswer)
        {
            return _stringHasher.VerifyHash(userAnswer, correntAnswer);
        }
    }
}