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
        public ActionResult Index(int? pageIndex)
        {
            var userId = this.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return this.View();
            }

            int pageSize = 50;

            var paginatedResults = new Dictionary<int, PaginatedCollection<TestResultEntryViewModel>>();
            var testResultListDto = this._resultsService.GetAllTestResults();

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
                            UserName = result.UserName,
                            Week1TimeSum = result.Week1TimeSum,
                            Week2TimeSum = result.Week2TimeSum,
                            Week3TimeSum = result.Week3TimeSum,
                            FinalTimeSum = result.FinalTimeSum
                        });
                    }

                    paginatedResults.Add(i, new PaginatedCollection<TestResultEntryViewModel>(totalTestResults, pageIndex ?? 1, pageSize));
                }
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