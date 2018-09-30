using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevAdventCalendarCompetition.Models;
using DevAdventCalendarCompetition.Vms;
using DevAdventCalendarCompetition.Data;
using System.Security.Claims;

namespace DevAdventCalendarCompetition.Controllers
{
    [Authorize]
    public class BaseTestController : Controller
    {
        protected ApplicationDbContext _context;

        public BaseTestController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult SaveAnswerAndRedirect(int testNumber)
        {
            var test = _context.Set<Test>().First(el => el.Number == testNumber);
            var testAnswer = new TestAnswer()
            {
                TestId = test.Id,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            AnsweringTime = DateTime.Now,
                AnsweringTimeOffset = DateTime.Now.Subtract(test.StartDate.Value)
            };

            _context.Set<TestAnswer>().Add(testAnswer);
            _context.SaveChanges();

            var vm = new AnswerVm() {TestAnswer = testAnswer, TestNumber = testNumber };
            return View("Answered", vm);
        }

    }
}