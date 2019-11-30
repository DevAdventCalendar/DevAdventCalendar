using System;
using System.Globalization;
using System.Security.Claims;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    public class TestController : BaseTestController
    {
        public TestController(IBaseTestService baseTestService)
            : base(baseTestService)
        {
        }

        [HttpGet]
        public ActionResult Index(int testNumber)
        {
            var test = this.baseTestService.GetTestByNumber(testNumber);
            var userHasAnswered = this.baseTestService.HasUserAnsweredTest(this.User.FindFirstValue(ClaimTypes.NameIdentifier), test.Id);
            if (userHasAnswered)
            {
                test.HasUserAnswered = true;
                return this.View(test);
            }
            else
            {
                if ((test.StartDate <= DateTime.Now && !(DateTime.Now > test.EndDate)) || !test.IsAdvent)
                {
                    return this.View(test);
                }
            }

            return this.View();
        }

        [HttpPost]
        public ActionResult Index(int testNumber, string answer = "")
        {
            if (answer is null)
            {
                throw new ArgumentNullException(nameof(answer));
            }

            var test = this.baseTestService.GetTestByNumber(testNumber);

            var finalAnswer = answer.ToUpper(CultureInfo.CurrentCulture).Replace(" ", " ", StringComparison.Ordinal);

            if (this.baseTestService.HasUserAnsweredTest(this.User.FindFirstValue(ClaimTypes.NameIdentifier), test.Id))
                    {
                        test.HasUserAnswered = true;
                        return this.View("Index", test);
                    }

            if (test.Status == TestStatus.Ended || test.Status == TestStatus.NotStarted)
                    {
                        this.ModelState.AddModelError(" ", "Wystąpił błąd!");
                        return this.View("Index", test);
                    }

            if (this.baseTestService.VerifyTestAnswer(finalAnswer, test.Answer))
            {
                        return this.SaveAnswerAndRedirect(testNumber);
            }

            this.SaveWrongAnswer(finalAnswer, testNumber);
            this.ModelState.AddModelError("Answer", "Źle! Spróbuj ponownie :)");

            return this.View("Index", test);
        }
    }
}