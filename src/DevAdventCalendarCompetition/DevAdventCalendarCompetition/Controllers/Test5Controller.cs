using DevAdventCalendarCompetition.Data;
using DevAdventCalendarCompetition.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DevAdventCalendarCompetition.Controllers
{
    public class Test5Controller : BaseTestController
    {
        public Test5Controller(ApplicationDbContext context) : base(context)
        {
        }

        [CanStartTest(TestNumber = 5)]
        public ActionResult Index()
        {
            var test = _context.Set<Test>().First(el => el.Number == 5);
            return View(test);
        }

        [HttpPost]
        [CanStartTest(TestNumber = 5)]
        public ActionResult Index(string answer = "")
        {
            var fixedAnswer = answer.ToUpper().Replace(" ", "").Replace(".", ",");
            double integralSolution = 0;
            double.TryParse(fixedAnswer, out integralSolution);
            integralSolution = Math.Truncate(integralSolution);

            if (integralSolution != 44.0)
            {
                ModelState.AddModelError("", "Answer is not correct. Try again.");
                var test = _context.Set<Test>().First(el => el.Number == 5);
                return View("Index", test);
            }

            return SaveAnswerAndRedirect(5);
        }
    }
}