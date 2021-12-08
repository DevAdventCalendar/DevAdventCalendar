using System;
using System.Globalization;
using System.Security.Claims;
using DevAdventCalendarCompetition.Models.Home;
using DevAdventCalendarCompetition.Providers;
using DevAdventCalendarCompetition.Resources;
using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        private readonly IAdventService _adventService;

        public HomeController(IHomeService homeService, IAdventService adventService)
        {
            this._homeService = homeService ?? throw new ArgumentNullException(nameof(homeService));
            this._adventService = adventService;
        }

        public ActionResult Index()
        {
            if (!this._adventService.IsAdvent())
            {
                return this.View();
            }

            var userId = this.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return this.View();
            }

            var currentTestsDto = this._homeService.GetCurrentTests();
            if (currentTestsDto == null)
            {
                return this.View();
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
        [Route("/ile_bombek_jest_na_choince")]
        public ActionResult HelpTheElves()
        {
            var answer = 45;

            this.Response.Headers.Add("X-Christmas-Tree", answer.ToString(new NumberFormatInfo()));
            return this.Json(ViewsMessages.HelpTheElves);
        }

        [HttpGet]
        [Route("/mysterious_name")]
        public ActionResult MysteriousName()
        {
            return this.Json(ViewsMessages.MysteriousName);
        }

        [HttpGet]
        [Route(nameof(Faq))]
        public ActionResult Faq()
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
        [Route(nameof(Partners))]
        public ActionResult Partners()
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