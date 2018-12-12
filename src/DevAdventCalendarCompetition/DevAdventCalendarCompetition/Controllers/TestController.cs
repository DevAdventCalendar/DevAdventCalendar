using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using DevAdventCalendarCompetition.Services.Models;

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
            if (test != null)
            {
                var userHasAnswered = _baseTestService.HasUserAnsweredTest(User.FindFirstValue(ClaimTypes.NameIdentifier), test.Id);
                if (userHasAnswered)
                {
                    test.HasUserAnswered = true;
                    return View(test);
                }
                else
                {
                    if (test.StartDate <= DateTime.Now && !(DateTime.Now > test.EndDate))
                    {
                        return View(test);
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(int testNumber, string answer = "")
        {
            var test = _baseTestService.GetTestByNumber(testNumber);
            var finalAnswer = answer.ToUpper().Replace(" ", "");

            if (test != null)
            {
                if (_baseTestService.HasUserAnsweredTest(User.FindFirstValue(ClaimTypes.NameIdentifier), test.Id))
                {
                    test.HasUserAnswered = true;
                    return View("Index", test);
                }   
                
                if(test.Status == TestStatus.Ended || test.Status == TestStatus.NotStarted)
                {
                    ModelState.AddModelError("", "Wystąpił błąd!");
                    return View("Index", test);
                }

                if(_baseTestService.VerifyTestAnswer(finalAnswer, test.Answer))        
                    return SaveAnswerAndRedirect(testNumber);

                SaveWrongAnswer(finalAnswer, testNumber);
                ModelState.AddModelError("", "Źle! Spróbuj ponownie :)");
            }
  
            return View("Index", test);
        }
    }
}