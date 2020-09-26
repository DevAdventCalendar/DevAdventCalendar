using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Models.GoogleCalendar;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using DevAdventCalendarCompetition.Services.Options;
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
        private readonly AdventSettings _adventSettings;

        public GoogleCalendarController(IGoogleCalendarService googleCalendarService, AdventSettings adventSettings)
        {
            this._googleCalendarService = googleCalendarService;
            this._adventSettings = adventSettings;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult AuthorizeAccessToGoogleCalendar()
        {
            return this.Challenge(
                new AuthenticationProperties
                {
                    RedirectUri = this.Url.Action(nameof(this.AuthorizationCallback), "GoogleCalendar")
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
            CalendarList calendars = new CalendarList();

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

        [HttpGet]
        public IActionResult AddCalendar()
        {
            var model = new AddCalendarViewModel
            {
                StartDate = this._adventSettings.StartDate,
                EndDate = this._adventSettings.EndDate
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCalendar(AddCalendarViewModel addCalendarViewModel)
        {
            if (addCalendarViewModel == null)
            {
                throw new ArgumentNullException(nameof(addCalendarViewModel));
            }

            if (this.ModelState.IsValid)
            {
                var response = await this._googleCalendarService.CreateNewCalendarWithEvents(
                    addCalendarViewModel.CalendarSummary,
                    addCalendarViewModel.StartDate,
                    addCalendarViewModel.EndDate);
                this.StatusMessage = response;
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(addCalendarViewModel);
        }

        private async Task<bool> CheckIfUserHasPermissions()
        {
            return await this.HttpContext.GetTokenAsync("Calendar", "access_token") != null;
        }
    }
}