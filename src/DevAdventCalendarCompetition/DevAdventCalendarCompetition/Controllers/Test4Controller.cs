using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DevAdventCalendarCompetition.Controllers
{
    public class Test4Controller : BaseTestController
    {
        public Test4Controller(IBaseTestService baseTestService) : base(baseTestService)
        {
        }

        [CanStartTest(TestNumber = 4)]
        public ActionResult Index()
        {
            var test = _baseTestService.GetTestByNumber(4);

            return View(test);
        }

        [HttpPost]
        [CanStartTest(TestNumber = 4)]
        public ActionResult Index(string answer = "")
        {
            var answers = new[] { "TALOFA", "FALOPA", "TALOFALAVA", "MALOLESOIFUA", "MALO" };
            var fixedAnswer = answer.ToUpper().Replace(" ", "");

            if (!answers.Contains(fixedAnswer))
            {
                ModelState.AddModelError("", "Answer is not correct. Try again.");

                var test = _baseTestService.GetTestByNumber(4);

                return View("Index", test);
            }

            return SaveAnswerAndRedirect(4);
        }
    }
}