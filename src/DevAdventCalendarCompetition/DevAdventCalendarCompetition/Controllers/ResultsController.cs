using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using DevAdventCalendarCompetition.Models.Home;
using DevAdventCalendarCompetition.Models.Test;
using DevAdventCalendarCompetition.Providers;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    [Route("[controller]/[action]")]
    public class ResultsController : Controller
    {
        private readonly IResultsService _resultsService;

        public ResultsController(IResultsService resultsService)
        {
            this._resultsService = resultsService ?? throw new ArgumentNullException(nameof(resultsService));
        }

        [HttpGet]
        public ActionResult Index()
        {
            var userId = this.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return this.View();
            }

            var userPosition = this._resultsService.GetUserPosition(userId);
            var vm = new TestResultsViewModel()
            {
                UserWeek1Position = userPosition.Week1Place ?? 0,
                UserWeek2Position = userPosition.Week2Place ?? 0,
                UserWeek3Position = userPosition.Week3Place ?? 0,
                UserFinalPosition = userPosition.FinalPlace ?? 0
            };

            return this.View(vm);
        }

        [HttpGet]
        public ActionResult RenderResults(int pageIndex = 1, int weekNumber = 1)
        {
            var userId = this.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return this.RedirectToAction("Index");
            }

            const int pageSize = 50;

            var testResultListDto = this._resultsService.GetTestResults(weekNumber, pageSize, pageIndex);
            if (testResultListDto == null || !testResultListDto.Any())
            {
                return this.PartialView("_ResultsPage", new KeyValuePair<int, PaginatedCollection<TestResultEntryViewModel>>(weekNumber, null));
            }

            var totalTestResults = testResultListDto
                    .Select(t => new TestResultEntryViewModel
                    {
                        Week1Points = t.Week1Points,
                        Week1Place = t.Week1Place,
                        Week2Points = t.Week2Points,
                        Week2Place = t.Week2Place,
                        Week3Points = t.Week3Points,
                        Week3Place = t.Week3Place,
                        FinalPoints = t.FinalPoints,
                        FinalPlace = t.FinalPlace,
                        UserId = t.UserId,
                        CorrectAnswers = t.CorrectAnswersCount,
                        WrongAnswers = t.WrongAnswersCount,
                        UserName = t.UserName,
                    })
                    .ToList();

            var totalPages = (int)Math.Ceiling(this._resultsService.GetTotalTestResultsCount(weekNumber) / (double)pageSize);

            var paginatedResults = new KeyValuePair<int, PaginatedCollection<TestResultEntryViewModel>>(
                weekNumber, new PaginatedCollection<TestResultEntryViewModel>(totalTestResults, pageIndex, pageSize, totalPages));

            return this.PartialView("_ResultsPage", paginatedResults);
        }
    }
}