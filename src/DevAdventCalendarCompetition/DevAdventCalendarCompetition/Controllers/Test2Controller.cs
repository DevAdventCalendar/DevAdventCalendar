using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    public class Test2Controller : BaseTestController
    {
        public Test2Controller(IBaseTestService baseTestService) : base(baseTestService)
        {
        }

        [CanStartTest(TestNumber = 2)]
        public ActionResult Index()
        {
            var test = _baseTestService.GetTestByNumber(2);

            return View(test);
        }

        [HttpPost]
        [CanStartTest(TestNumber = 2)]
        public ActionResult Index(string answer = "")
        {
            var testNumber = 2;
            var fixedAnswer = answer.ToUpper().Replace(" ", "");

            if (fixedAnswer != "ANVQOFUHUFKUESDQMF")
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