using DevAdventCalendarCompetition.Data;
using DevAdventCalendarCompetition.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DevAdventCalendarCompetition.Controllers
{
    public class Test6Controller : BaseTestController
    {
        public Test6Controller(ApplicationDbContext context) : base(context)
        {
        }

        [CanStartTest(TestNumber = 6)]
        public ActionResult Index()
        {
            var test = _context.Set<Test>().First(el => el.Number == 6);
            return View(test);
        }

        [HttpPost]
        [CanStartTest(TestNumber = 6)]
        public ActionResult Index(string answer = "")
        {
            var fixedAnswer = answer.ToUpper().Replace(" ", "");

            if (fixedAnswer != "PEOPLE")
            {
                ModelState.AddModelError("", "Answer is not correct. Try again.");
                var test = _context.Set<Test>().First(el => el.Number == 6);
                return View("Index", test);
            }

            return SaveAnswerAndRedirect(6);
        }
    }
}