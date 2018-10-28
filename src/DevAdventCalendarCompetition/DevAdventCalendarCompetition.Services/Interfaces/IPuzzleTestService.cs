using System;
using System.Collections.Generic;
using System.Text;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IPuzzleTestService
    {
        TestAnswerDto GetEmptyAnswerForStartedTestByUser(string userId);
        void SaveEmptyTestAnswer(int testId, string userId);
        void UpdatePuzzleTestAnswer(TestAnswerDto testAnswerDto, int moves, int gameDuration, string testEnd);
        void ResetGame(TestAnswerDto testAnswerDto, int moveCount);
    }
}
