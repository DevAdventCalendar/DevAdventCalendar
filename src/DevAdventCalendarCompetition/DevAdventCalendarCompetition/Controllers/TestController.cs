using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    public class TestController : BaseTestController
    {
        public TestController(IBaseTestService baseTestService) : base(baseTestService)
        {
        }

        [HttpGet]
        public ActionResult Index(int testNumber)
        {
            var test = _baseTestService.GetTestByNumber(testNumber);

            return View(test);
        }

        [HttpPost]
        public ActionResult Index(string answer = "")
        {
            var testNumber = 1;

            var fixedAnswer = answer.ToUpper().Replace(" ", "");

            if (fixedAnswer != "0100111101000010010010100100010101000011010101000100100101010110010010010101010001011001")
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