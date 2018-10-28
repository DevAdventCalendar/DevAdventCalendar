using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Data;
using DevAdventCalendarCompetition.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DevAdventCalendarCompetition.Controllers
{
    public class PuzzleTestController : BaseTestController
    {
        public PuzzleTestController(IBaseTestService baseTestService, IPuzzleTestService puzzleTestService) : base(baseTestService)
        {
            _puzzleTestService = puzzleTestService;
        }

        [HttpGet]
        [CanStartTest(TestNumber = 7)]
        public IActionResult Index()
        {
            var test = _baseTestService.GetTestByNumber(7);
            return View(test);
        }

        [HttpGet]
        public IActionResult CheckGameStarted()
        {
            var testAnswer =_puzzleTestService.GetEmptyAnswerForStartedTestByUser(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (testAnswer != null)
                return Ok(true);
            else
                return BadRequest(false);
        }

        [HttpPost]
        [CanStartTest(TestNumber = 7)]
        public IActionResult StartGame()
        {
            var test = _baseTestService.GetTestByNumber(7);

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
            var testAnswer = _puzzleTestService.GetEmptyAnswerForStartedTestByUser(User.FindFirstValue(ClaimTypes.NameIdentifier));

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
            var testAnswer = _puzzleTestService.GetEmptyAnswerForStartedTestByUser(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (testAnswer != null)
            {
                _puzzleTestService.ResetGame(testAnswer, moveCount);

                return Ok();
            }

            return BadRequest();
        }
    }
}