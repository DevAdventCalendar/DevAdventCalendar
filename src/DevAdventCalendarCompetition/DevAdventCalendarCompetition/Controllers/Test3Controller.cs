using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    public class Test3Controller : BaseTestController
    {
        public Test3Controller(IBaseTestService baseTestService) : base(baseTestService)
        {
        }

        [CanStartTest(TestNumber = 3)]
        public ActionResult Index()
        {
            var test = _baseTestService.GetTestByNumber(3);

            return View(test);
        }

        [HttpPost]
        [CanStartTest(TestNumber = 3)]
        public ActionResult Index(string answer = "")
        {
            var testNumber = 3;
            var fixedAnswer = answer.ToUpper().Replace(" ", "");

            if (fixedAnswer != "READYMIX")
            {
                SaveWrongAnswer(fixedAnswer, testNumber);

                ModelState.AddModelError("", "Answer is not correct. Try again.");

                var test = _baseTestService.GetTestByNumber(testNumber);

                return View("Index", test);
            }

            return SaveAnswerAndRedirect(testNumber);
        }
    }
}