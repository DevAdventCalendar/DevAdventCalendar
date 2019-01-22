using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AutoMapper;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services
{
    public class PuzzleTestService : IPuzzleTestService
    {
        private readonly IPuzzleTestRepository _puzzleTestRepository;
        private readonly IBaseTestRepository _baseTestRepository;

        public PuzzleTestService(IPuzzleTestRepository puzzleTestRepository, IBaseTestRepository baseTestRepository)
        {
            _puzzleTestRepository = puzzleTestRepository;
            _baseTestRepository = baseTestRepository;
        }

        public TestAnswerDto GetEmptyAnswerForStartedTestByUser(string userId)
        {
            var testAnswer = _puzzleTestRepository.GetEmptyAnswerForStartedTestByUser(userId);
            var testAnswerDto = Mapper.Map<TestAnswerDto>(testAnswer);
            return testAnswerDto;
        }

        public void SaveEmptyTestAnswer(int testId, string userId)
        {
            var testAnswer = new TestAnswer
            {
                //Answer = null, TODO: Correct checking for null answer
                TestId = testId,
                UserId = userId,
                AnsweringTime = DateTime.MinValue,
                AnsweringTimeOffset = TimeSpan.Zero
            };

            _baseTestRepository.AddAnswer(testAnswer);
        }

        public void UpdatePuzzleTestAnswer(TestAnswerDto testAnswerDto, int moves, int gameDuration, string testEnd)
        {
            var testAnswer = _puzzleTestRepository.GetEmptyAnswerForStartedTestByUser(testAnswerDto.UserId);
            //testAnswer.Answer = moves.ToString(); TODO: Correct checking for null answer
            testAnswer.AnsweringTime = DateTime.ParseExact(testEnd, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
            testAnswer.AnsweringTimeOffset = TimeSpan.FromSeconds(gameDuration);

            _baseTestRepository.UpdateAnswer(testAnswer);

        }

        public void ResetGame(TestAnswerDto testAnswerDto, int moveCount)
        {
            var testAnswer = _puzzleTestRepository.GetEmptyAnswerForStartedTestByUser(testAnswerDto.UserId);

            // testAnswer.Answer = moveCount.ToString(); TODO: Correct checking for null answer
            testAnswer.AnsweringTime = DateTime.Now;
            testAnswer.AnsweringTimeOffset = TimeSpan.Zero;

            _baseTestRepository.UpdateAnswer(testAnswer);
        }
    }
}

