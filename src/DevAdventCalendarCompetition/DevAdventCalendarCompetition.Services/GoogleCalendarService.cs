using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using DevAdventCalendarCompetition.Services.Models.GoogleCalendar;
using DevAdventCalendarCompetition.Services.Options;

namespace DevAdventCalendarCompetition.Services
{
    public class GoogleCalendarService : IGoogleCalendarService
    {
        private readonly HttpClient _httpClient;
        private readonly AdventSettings _adventSettings;

        public GoogleCalendarService(HttpClient httpClient, AdventSettings adventSettings)
        {
            this._httpClient = httpClient;
            this._adventSettings = adventSettings;
        }

        public async Task<OperationalResult> CreateNewCalendarWithEvents()
        {
            var calendarSummary = "DevAdventCalendar";
            var calendarResponse = await this.CreateCalendar(calendarSummary);
            if (!calendarResponse.IsSuccessStatusCode)
            {
                return OperationalResult.Failure(OperationalResultStatus.CalendarFailure);
            }

            var newCalendar = await calendarResponse.Content.ReadFromJsonAsync<CalendarDto>();
            var eventsResponse = await this.CreateEvents(newCalendar.Id);

            if (eventsResponse.IsSuccessStatusCode)
            {
                return OperationalResult.Success();
            }

            return OperationalResult.Failure(OperationalResultStatus.EventsFailure);
        }

        private async Task<HttpResponseMessage> CreateCalendar(string calendarSummary)
        {
            var calendarsUrl = "calendars";
            var newCalendar = new NewCalendarDto
            {
                Summary = calendarSummary
            };
            return await this._httpClient.PostAsJsonAsync(calendarsUrl, newCalendar);
        }

        private async Task<HttpResponseMessage> CreateEvents(string calendarId)
        {
            var summary = "DevAdventCalendar";
            var location = @"https://devadventcalendar.pl/";
            var timeZone = @"Europe/Warsaw";
            var startDate = this._adventSettings.StartDate;
            var endDate = this._adventSettings.EndDate;
            var daysCount = (endDate.Day - startDate.Day) + 1;
            var recurrence = $"RRULE:FREQ=DAILY;COUNT={daysCount}";
            var calendarEventsUrl = $"calendars/{calendarId}/events";

            var newEvents = new EventsDto
            {
                Summary = summary,
                Location = location,
                Start = new EventDate
                {
                    DateTime = startDate,
                    TimeZone = timeZone
                },
                End = new EventDate
                {
                    DateTime = startDate.AddHours(1),
                    TimeZone = timeZone
                },
                Recurrence = new List<string>()
                {
                    recurrence
                }
            };
            return await this._httpClient.PostAsJsonAsync(calendarEventsUrl, newEvents);
        }
    }
}