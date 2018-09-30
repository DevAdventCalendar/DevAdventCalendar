using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DevAdventCalendarCompetition.Models;
using DevAdventCalendarCompetition.Data;

namespace DevAdventCalendarCompetition.Controllers
{
    public class Test3Controller : BaseTestController
    {
        public Test3Controller(ApplicationDbContext context) : base(context)
        {
        }

        [CanStartTest(TestNumber = 3)]
        public ActionResult Index()
        {
            var test = _context.Set<Test>().First(el => el.Number == 3);
            return View(test);
        }

        [HttpPost]
        [CanStartTest(TestNumber = 3)]
        public ActionResult Index(string answer = "")
        {
            var fixedAnswer = answer.ToUpper().Replace(" ", "");

            if (fixedAnswer != "READYMIX")
            {
                ModelState.AddModelError("", "Answer is not correct. Try again.");
                var test = _context.Set<Test>().First(el => el.Number == 3);
                return View("Index", test);
            }

            return SaveAnswerAndRedirect(3);
        }
    }
}