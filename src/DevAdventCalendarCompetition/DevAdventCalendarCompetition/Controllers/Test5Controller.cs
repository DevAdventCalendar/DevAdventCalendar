using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DevAdventCalendarCompetition.Controllers
{
    public class Test5Controller : BaseTestController
    {
        public Test5Controller(IBaseTestService baseTestService) : base(baseTestService)
        {
        }

        [CanStartTest(TestNumber = 5)]
        public ActionResult Index()
        {
            var test = _baseTestService.GetTestByNumber(5);

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
				SaveWrongAnswer(fixedAnswer);

				ModelState.AddModelError("", "Answer is not correct. Try again.");

                var test = _baseTestService.GetTestByNumber(5);

                return View("Index", test);
            }

            return SaveAnswerAndRedirect(5);
        }
    }
}