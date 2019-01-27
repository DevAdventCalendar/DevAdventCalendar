using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AutoMapper;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using Newtonsoft.Json;

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

        public TestAnswerDto GetEmptyAnswerForStartedTestByUser(string userId, int testId)
        {
            var testAnswer = _puzzleTestRepository.GetEmptyAnswerForStartedTestByUser(userId, testId);
            var testAnswerDto = Mapper.Map<TestAnswerDto>(testAnswer);
            return testAnswerDto;
        }

        public void SaveEmptyTestAnswer(int testId, string userId)
        {
            var testAnswer = new TestAnswer
            {
                PlainAnswer = null,
                TestId = testId,
                UserId = userId,
                AnsweringTime = DateTime.MinValue,
                AnsweringTimeOffset = TimeSpan.Zero
            };

            _baseTestRepository.AddAnswer(testAnswer);
        }

        public void UpdatePuzzleTestAnswer(TestAnswerDto testAnswerDto, int moves, int gameDuration, string testEnd)
        {
            var testAnswer = _puzzleTestRepository.GetEmptyAnswerForStartedTestByUser(testAnswerDto.UserId, testAnswerDto.TestId);
            testAnswer.PlainAnswer = JsonConvert.SerializeObject(new
            {
                MoveCount = moves.ToString(),
                GameDuration = gameDuration.ToString()
            });
           
            var maxAnswerTime = new TimeSpan(0, 23, 59, 59, 999);     
            var answerTimeOffset = TimeSpan.FromSeconds(gameDuration);

            testAnswer.AnsweringTime = DateTime.Now;
            testAnswer.AnsweringTimeOffset = answerTimeOffset > maxAnswerTime ? maxAnswerTime : answerTimeOffset;

            _baseTestRepository.UpdateAnswer(testAnswer);

        }

        public void ResetGame(TestAnswerDto testAnswerDto, int moveCount)
        {
            var testAnswer = _puzzleTestRepository.GetEmptyAnswerForStartedTestByUser(testAnswerDto.UserId, testAnswerDto.TestId);

            testAnswer.PlainAnswer = JsonConvert.SerializeObject(new
            {
                MoveCount = moveCount.ToString(),
                GameDuration = string.Empty
            });

            testAnswer.AnsweringTime = DateTime.Now;
            testAnswer.AnsweringTimeOffset = TimeSpan.Zero;

            _baseTestRepository.UpdateAnswer(testAnswer);
        }
    }
}

