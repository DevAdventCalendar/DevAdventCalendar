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
    [Route("[controller]/[action]")]
    public class ResultsController : Controller
    {
        private readonly IResultsService _resultsService;

        public ResultsController(IResultsService resultsService)
        {
            this._resultsService = resultsService ?? throw new ArgumentNullException(nameof(resultsService));
        }

        [HttpGet]
        public ActionResult Index(int? pageIndex, int weekNumber = 4)
        {
            var userId = this.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return this.View();
            }

            int pageSize = 50;

            var paginatedResults = new Dictionary<int, PaginatedCollection<TestResultEntryViewModel>>();
            var testResultListDto = this._resultsService.GetTestResults(weekNumber);

            for (var i = 1; i <= testResultListDto.Count; i++)
            {
                var totalTestResults = new List<TestResultEntryViewModel>
                {
                    new TestResultEntryViewModel
                    {
                        Week1Points = testResultListDto[i].Week1Points,
                        Week1Place = testResultListDto[i].Week1Place,
                        Week2Points = testResultListDto[i].Week2Points,
                        Week2Place = testResultListDto[i].Week2Place,
                        Week3Points = testResultListDto[i].Week3Points,
                        Week3Place = testResultListDto[i].Week3Place,
                        FinalPoints = testResultListDto[i].FinalPoints,
                        FinalPlace = testResultListDto[i].FinalPlace,
                        UserId = testResultListDto[i].UserId,
                        CorrectAnswers = testResultListDto[i].CorrectAnswersCount,
                        WrongAnswers = testResultListDto[i].WrongAnswersCount,
                        UserName = testResultListDto[i].UserName,
                    }
                };

                paginatedResults.Add(i, new PaginatedCollection<TestResultEntryViewModel>(totalTestResults, pageIndex ?? 1, pageSize));
            }

            var userPosition = this._resultsService.GetUserPosition(userId);
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
    }
}