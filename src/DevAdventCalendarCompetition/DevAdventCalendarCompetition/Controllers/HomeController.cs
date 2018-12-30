using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Vms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace DevAdventCalendarCompetition.Controllers
{
    public class HomeController : Controller
    {
        protected IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public ActionResult Index()
        {
            var currentTestsDto = _homeService.GetCurrentTests();
            if (currentTestsDto == null)
                return View();

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return View(currentTestsDto);

            ViewBag.CorrectAnswers = _homeService.GetCorrectAnswersCountForUser(userId);

            return View(currentTestsDto);
        }

        public ActionResult Results()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return View();

            var testDtoList = _homeService.GetTestsWithUserAnswers();

            var singleTestResults = testDtoList.Select(testDto => new SingleTestResultsVm()
            {
                TestNumber = testDto.Number,
                TestEnded = testDto.HasEnded,
                EndDate = testDto.EndDate,
                StartDate = testDto.StartDate,
                Entries = testDto.Answers
                    .Select(
                        ta =>
                            new SingleTestResultEntry()
                            {
                                UserId = ta.UserId,
                                FullName = PrepareUserEmailForRODO(ta.UserFullName),
                                CorrectAnswersCount = testDto.Answers.Count(a => a.UserId == ta.UserId),
                                WrongAnswersCount = testDto.WrongAnswers.Count(w => w.UserId == ta.UserId)
                            })
                    .Union(testDto.WrongAnswers
                    .Select(
                        wa =>
                            new SingleTestResultEntry()
                            {
                                UserId = wa.UserId,
                                FullName = PrepareUserEmailForRODO(wa.UserFullName),
                                CorrectAnswersCount = testDto.Answers.Count(a => a.UserId == wa.UserId),
                                WrongAnswersCount = testDto.WrongAnswers.Count(w => w.UserId == wa.UserId)
                            }))
                    .GroupBy(e => new { e.FullName, e.CorrectAnswersCount, e.WrongAnswersCount })
                    .Select(e => new SingleTestResultEntry
                    {
                        FullName = e.Key.FullName,
                        CorrectAnswersCount = e.Key.CorrectAnswersCount,
                        WrongAnswersCount = e.Key.WrongAnswersCount
                    })
                    .OrderByDescending(e => e.CorrectAnswersCount)
                    .ToList()
            }).ToList();

            var testResultListDto = _homeService.GetAllTestResults();

            List<TotalTestResultEntryVm> totalTestResults = new List<TotalTestResultEntryVm>();

            foreach (var result in testResultListDto)
            {
                totalTestResults.Add(new TotalTestResultEntryVm
                {
                    Position = result.Position,
                    UserId = result.UserId,
                    FullName = PrepareUserEmailForRODO(result.Email),
                    TotalPoints = result.Points
                });
            }          

            var vm = new TestResultsVm()
            {
                CurrentUserPosition = _homeService.GetUserPosition(userId),
                SingleTestResults = singleTestResults,
                TotalTestResults = totalTestResults
            };

            return View("Results", vm);
        }

        [HttpPost]
        public ActionResult CheckTestStatus(int testNumber)
        {
            return Content(_homeService.CheckTestStatus(testNumber));  
        }

        public ActionResult Error()
        {
            ViewBag.errorMessage = TempData["errorMessage"];
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Sponsors()
        {
            return View();
        }

        public ActionResult Prizes()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult Rules()
        {
            return View();
        }

        public ActionResult TestHasNotStarted(int number)
        {
            return View(number);
        }

        public ActionResult TestHasEnded(int number)
        {
            return View(number);
        }

        public ActionResult TestAnswered(int number)
        {
            return View(number);
        }

        public ActionResult GetServerTime()
        {
            return Json(DateTime.Now.ToString("yyyy'-'MM'-'ddTHH':'mm':'ss.fff%K"));
        }

        private string PrepareUserEmailForRODO(string email)
        {
            string emailMaskRegex = "(^\\w{3}).*(@\\w).*(.)$";

            return Regex.Replace(email, emailMaskRegex,
                m => m.Groups[1].Value + "..." + m.Groups[2].Value + "..." + m.Groups[3].Value);
        }
    }
}