using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Data;
using DevAdventCalendarCompetition.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGameResult([FromBody] string result, DateTime testEnd)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var test = _context.Tests.FirstOrDefault(t => t.Id == 7);
            var user = _userManager.Users.FirstOrDefault(u => u.Email == HttpContext.User.Identity.Name);

            var answer = new TestAnswer
            {
                Test = test,
                TestId = test.Id,
                User = user,
                UserId = user.Id,
                Answer = result,
                AnsweringTime = testEnd
            };

            _context.TestAnswers.Add(answer);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}