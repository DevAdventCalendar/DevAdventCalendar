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

        [HttpPost]
        public ActionResult CheckTestStatus(int testNumber)
        {
            return this.Content(this._homeService.CheckTestStatus(testNumber));
        }

        [Route(nameof(Error))]
        public ActionResult Error([FromQuery]int statusCode)
        {
            return this.View(new ErrorViewModel
            {
                Message = ErrorMessagesProvider.GetMessageBody(statusCode)
            });
        }

        [Route("11111100011")]
        public ActionResult Surprise()
        {
            return this.View();
        }

        [Route(nameof(About))]
        public ActionResult About()
        {
            return this.View();
        }

        [Route(nameof(Contact))]
        public ActionResult Contact()
        {
            return this.View();
        }

        [Route(nameof(Sponsors))]
        public ActionResult Sponsors()
        {
            return this.View();
        }

        [Route(nameof(Prizes))]
        public ActionResult Prizes()
        {
            return this.View();
        }

        [Route(nameof(Rules))]
        public ActionResult Rules()
        {
            return this.View();
        }

        public ActionResult TestHasNotStarted(int number)
        {
            return this.View(number);
        }

        public ActionResult TestHasEnded(int number)
        {
            return this.View(number);
        }

        public ActionResult TestAnswered(int number)
        {
            return this.View(number);
        }

        public ActionResult GetServerTime()
        {
            return this.Json(DateTime.Now.ToString("yyyy'-'MM'-'ddTHH':'mm':'ss.fff%K", CultureInfo.InvariantCulture));
        }
    }
}