using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using DevAdventCalendarCompetition.Models.Home;
using DevAdventCalendarCompetition.Models.Test;
using DevAdventCalendarCompetition.Providers;
using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            this._homeService = homeService ?? throw new ArgumentNullException(nameof(homeService));
        }

        public ActionResult Index()
        {
            var currentTestsDto = this._homeService.GetCurrentTests();
            if (currentTestsDto == null)
            {
                return this.View();
            }

            var userId = this.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return this.View(currentTestsDto);
            }

            this.ViewBag.CorrectAnswers = this._homeService.GetCorrectAnswersCountForUser(userId);

            return this.View(currentTestsDto);
        }

        [HttpGet]
        [Route("Results")]
        public ActionResult Results(int? pageIndex)
        {
            var userId = this.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return this.View();
            }

            int pageSize = 50;

            var paginatedResults = new Dictionary<int, PaginatedCollection<TestResultEntryViewModel>>();
            var testResultListDto = this._homeService.GetAllTestResults();

            for (var i = 1; i <= testResultListDto.Count; i++)
            {
                if (testResultListDto.TryGetValue(i, out var results))
                {
                    var totalTestResults = new List<TestResultEntryViewModel>();

                    foreach (var result in results)
                    {
                        totalTestResults.Add(new TestResultEntryViewModel
                        {
                            Week1Points = result.Week1Points,
                            Week1Place = result.Week1Place,
                            Week2Points = result.Week2Points,
                            Week2Place = result.Week2Place,
                            Week3Points = result.Week3Points,
                            Week3Place = result.Week3Place,
                            FinalPoints = result.FinalPoints,
                            FinalPlace = result.FinalPlace,
                            UserId = result.UserId,
                            CorrectAnswers = result.CorrectAnswersCount,
                            WrongAnswers = result.WrongAnswersCount,
                            FullName = this._homeService.PrepareUserEmailForRODO(result.Email),
                        });
                    }

                    paginatedResults.Add(i, new PaginatedCollection<TestResultEntryViewModel>(totalTestResults, pageIndex ?? 1, pageSize));
                }
            }

            var userPosition = this._homeService.GetUserPosition(userId);
            var vm = new TestResultsViewModel()
            {
                UserWeek1Position = userPosition.Week1Place ?? 0,
                UserWeek2Position = userPosition.Week2Place ?? 0,
                UserWeek3Position = userPosition.Week3Place ?? 0,
                UserFinalPosition = userPosition.FinalPlace ?? 0,
                TotalTestResults = paginatedResults
            };

            return this.View(vm);
        }

        [HttpGet]
        public ActionResult CheckTestStatus(int testNumber)
        {
            return this.Content(this._homeService.CheckTestStatus(testNumber));
        }

        [HttpGet]
        [Route(nameof(Error))]
        public ActionResult Error([FromQuery]int statusCode)
        {
            return this.View(new ErrorViewModel
            {
                Message = ErrorMessagesProvider.GetMessageBody(statusCode)
            });
        }

        [HttpGet]
        [Route("11111100011")]
        public ActionResult Surprise()
        {
            return this.View();
        }

        [HttpGet]
        [Route(nameof(About))]
        public ActionResult About()
        {
            return this.View();
        }

        [HttpGet]
        [Route(nameof(Contact))]
        public ActionResult Contact()
        {
            return this.View();
        }

        [HttpGet]
        [Route(nameof(Sponsors))]
        public ActionResult Sponsors()
        {
            return this.View();
        }

        [HttpGet]
        [Route(nameof(Prizes))]
        public ActionResult Prizes()
        {
            return this.View();
        }

        [HttpGet]
        [Route(nameof(Rules))]
        public ActionResult Rules()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult TestHasNotStarted(int number)
        {
            return this.View(number);
        }

        [HttpGet]
        public ActionResult TestHasEnded(int number)
        {
            return this.View(number);
        }

        [HttpGet]
        public ActionResult TestAnswered(int number)
        {
            return this.View(number);
        }

        [HttpGet]
        public ActionResult GetServerTime()
        {
            return this.Json(DateTime.Now.ToString("yyyy'-'MM'-'ddTHH':'mm':'ss.fff%K", CultureInfo.InvariantCulture));
        }
    }
}