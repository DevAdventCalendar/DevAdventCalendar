using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Models.GoogleCalendar;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    [Authorize]
    [Route("Manage/[controller]/[action]")]
    public class GoogleCalendarController : Controller
    {
        private readonly IGoogleCalendarService _googleCalendarService;

        public GoogleCalendarController(IGoogleCalendarService googleCalendarService)
        {
            this._googleCalendarService = googleCalendarService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult AuthorizeAccessToGoogleCalendar()
        {
            var redirectUri = this.Url.Action(nameof(this.AuthorizationCallback), "GoogleCalendar");
            return this.Challenge(
                new AuthenticationProperties
                {
                    RedirectUri = redirectUri
                },
                "Calendar");
        }

        public IActionResult AuthorizationCallback()
        {
            this.StatusMessage = "Autoryzacja została uzyskana";
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userHasPermissions = await this.CheckIfUserHasPermissions();
            IEnumerable<Items> calendars = new List<Items>();

            if (userHasPermissions)
            {
                calendars = await this._googleCalendarService.GetAllCalendars();
            }

            var model = new IndexViewModel
            {
                Calendars = calendars,
                HasPermissions = userHasPermissions,
                StatusMessage = this.StatusMessage
            };

            return this.View(model);
        }

        private async Task<bool> CheckIfUserHasPermissions()
        {
            return await this.HttpContext.GetTokenAsync("Calendar", "access_token") != null;
        }
    }
}