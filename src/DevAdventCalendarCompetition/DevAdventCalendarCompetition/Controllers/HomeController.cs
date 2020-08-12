using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using DevAdventCalendarCompetition.Models.Home;
using DevAdventCalendarCompetition.Models.Test;
using DevAdventCalendarCompetition.Providers;
using DevAdventCalendarCompetition.Services.Extensions;
using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DevAdventCalendarCompetition.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        private readonly IConfiguration _configuration;

        public HomeController(IHomeService homeService, IConfiguration configuration)
        {
            this._homeService = homeService ?? throw new ArgumentNullException(nameof(homeService));
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public ActionResult Index()
        {
            this.ViewBag.IsAdvent = IsAdventExtensions.CheckIsAdvent(this._configuration);

            if (IsAdventExtensions.CheckIsAdvent(this._configuration) == false)
            {
                return this.View();
            }

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