using System.Security.Claims;
using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    public class PuzzleTestController : BaseTestController
    {
        private readonly IPuzzleTestService _puzzleTestService;

        public PuzzleTestController(IBaseTestService baseTestService, IPuzzleTestService puzzleTestService) : base(baseTestService)
        {
            _puzzleTestService = puzzleTestService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var test = _baseTestService.GetTestByNumber(0);
            return View(test);
        }

        [HttpGet]
        public IActionResult CheckGameStarted()
        {
            var puzzleTest = _baseTestService.GetTestByNumber(0);
            var testAnswer =_puzzleTestService.GetEmptyAnswerForStartedTestByUser(User.FindFirstValue(ClaimTypes.NameIdentifier), puzzleTest.Id);

            if (testAnswer != null)
                return Ok(true);

            return BadRequest(false);
        }

        [HttpPost]
        public IActionResult StartGame()
        {
            var test = _baseTestService.GetTestByNumber(0);

            if (test != null)
            {
                _puzzleTestService.SaveEmptyTestAnswer(test.Id, User.FindFirstValue(ClaimTypes.NameIdentifier));
                return Ok("Test started!");            
            }

            return BadRequest();
        }
    
        // TODO: Secure the game from changing values in view and possible winning the game without any effort.

        [HttpPost]
        public IActionResult UpdateGameResult(int moveCount, int gameDuration, string testEnd)
        {
            var puzzleTest = _baseTestService.GetTestByNumber(0);
            var testAnswer = _puzzleTestService.GetEmptyAnswerForStartedTestByUser(User.FindFirstValue(ClaimTypes.NameIdentifier), puzzleTest.Id);

            if (testAnswer != null)
            {
                _puzzleTestService.UpdatePuzzleTestAnswer(testAnswer, moveCount, gameDuration, testEnd);

                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult ResetGame(int moveCount)
        {
            var puzzleTest = _baseTestService.GetTestByNumber(0);
            var testAnswer = _puzzleTestService.GetEmptyAnswerForStartedTestByUser(User.FindFirstValue(ClaimTypes.NameIdentifier), puzzleTest.Id);

            if (testAnswer != null)
            {
                _puzzleTestService.ResetGame(testAnswer, moveCount);

                return Ok();
            }

            return BadRequest();
        }
    }
}