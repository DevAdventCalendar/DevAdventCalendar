using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Data;
using DevAdventCalendarCompetition.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DevAdventCalendarCompetition.Controllers
{
    public class PuzzleTestController : BaseTestController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public PuzzleTestController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [CanStartTest(TestNumber = 7)]
        public IActionResult Index()
        {
            var test = _context.Tests.FirstOrDefault(el => el.Number == 7);
            return View(test);
        }

        [HttpGet]
        public async Task<IActionResult> CheckGameStarted()
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == HttpContext.User.Identity.Name);

            if (user != null)
            {
                var test = await _context.TestAnswers.FirstOrDefaultAsync(a => a.AnsweringTime == DateTime.MinValue && a.Answer == null && a.UserId == user.Id);

                if (test != null)
                {
                    return Ok(true);
                }
            }

            return Ok(false);
        }

        [HttpPost]
        [CanStartTest(TestNumber = 7)]
        public async Task<IActionResult> StartGame()
        {
            var test = await _context.Tests.FirstOrDefaultAsync(t => t.Number == 7);

            if (test != null)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == HttpContext.User.Identity.Name);

                if (user != null)
                {
                    var answer = new TestAnswer
                    {
                        Answer = null,
                        Test = test,
                        TestId = test.Id,
                        User = user,
                        UserId = user.Id
                    };

                    _context.TestAnswers.Add(answer);
                    await _context.SaveChangesAsync();

                    return Ok("Test started!");
                }              
            }

            return BadRequest();
        }
    
        // TODO: Secure the game from changing values in view and possible winning the game without any effort.

        [HttpPost]
        public async Task<IActionResult> UpdateGameResult(int moves, int gameDuration, string testEnd)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Email == HttpContext.User.Identity.Name);
           
            if (user != null)
            {
                var testAnswer = await _context.TestAnswers.FirstOrDefaultAsync(a => a.AnsweringTime == DateTime.MinValue && a.Answer == null && a.UserId == user.Id);

                if (testAnswer != null)
                {
                    testAnswer.Answer = moves.ToString();
                    testAnswer.AnsweringTime = DateTime.ParseExact(testEnd, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                    testAnswer.AnsweringTimeOffset = TimeSpan.FromSeconds(gameDuration);

                    _context.TestAnswers.Update(testAnswer);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> ResetGame(int moveCount)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Email == HttpContext.User.Identity.Name);

            if (user != null)
            {
                var testAnswer = await _context.TestAnswers.FirstOrDefaultAsync(a =>
                    a.AnsweringTime == DateTime.MinValue && a.Answer == null && a.UserId == user.Id);

                if (testAnswer != null)
                {
                    testAnswer.Answer = moveCount.ToString();
                    testAnswer.AnsweringTime = DateTime.Now;
                    testAnswer.AnsweringTimeOffset = TimeSpan.Zero;

                    _context.TestAnswers.Update(testAnswer);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
            }

            return BadRequest();
        }
    }
}