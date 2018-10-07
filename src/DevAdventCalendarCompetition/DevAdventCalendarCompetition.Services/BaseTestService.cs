using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using System;

namespace DevAdventCalendarCompetition.Services
{
    public class BaseTestService : IBaseTestService
    {
        private readonly IBaseTestRepository _baseTestRepository;

        public BaseTestService(IBaseTestRepository baseTestRepository)
        {
            _baseTestRepository = baseTestRepository;
        }

        public Test GetTestByNumber(int testNumber)
        {
            return _baseTestRepository.GetByNumber(testNumber);
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

        public TestAnswer GetAnswerByTestId(int testId)
        {
            return _baseTestRepository.GetAnswerByTestId(testId);
        }
    }
}