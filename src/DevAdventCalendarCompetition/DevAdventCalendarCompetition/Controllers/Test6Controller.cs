using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    public class Test6Controller : BaseTestController
    {
        public Test6Controller(IBaseTestService baseTestService) : base(baseTestService)
        {
        }

        [CanStartTest(TestNumber = 6)]
        public ActionResult Index()
        {
            var test = _baseTestService.GetTestByNumber(6);

            return View(test);
        }

        [HttpPost]
        [CanStartTest(TestNumber = 6)]
        public ActionResult Index(string answer = "")
        {
            var testNumber = 6;
            var fixedAnswer = answer.ToUpper().Replace(" ", "");

            if (fixedAnswer != "PEOPLE")
            {
                SaveWrongAnswer(fixedAnswer, testNumber);

                ModelState.AddModelError("", "Odpowiedź jest nieprawidłowa. Spróbuj ponownie.");

                var test = _baseTestService.GetTestByNumber(testNumber);

                return View("Index", test);
            }

            return SaveAnswerAndRedirect(testNumber);
        }
    }
}