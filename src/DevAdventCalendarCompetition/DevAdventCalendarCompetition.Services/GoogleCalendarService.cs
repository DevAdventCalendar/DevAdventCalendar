using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Transactions;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services
{
    public class GoogleCalendarService : IGoogleCalendarService
    {
        private readonly HttpClient _httpClient;

        public GoogleCalendarService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<CalendarList> GetAllCalendars()
        {
            return await this._httpClient.GetFromJsonAsync<CalendarList>("users/me/calendarList");
        }

        public async Task<string> CreateNewCalendarWithEvents(string calendarSummary, DateTime startDate, DateTime endDate)
        {
            var calendarResponse = await this.CreateCalendar(calendarSummary);

            // For debug
            calendarResponse.EnsureSuccessStatusCode();

            var newCalendar = await calendarResponse.Content.ReadFromJsonAsync<CalendarDto>();

            var eventsResponse = await this.CreateEvents(newCalendar.Id, startDate, endDate);
            eventsResponse.EnsureSuccessStatusCode();

            if (calendarResponse.IsSuccessStatusCode && eventsResponse.IsSuccessStatusCode)
            {
                return $"Pomyślnie utworzono kalendarz: {calendarSummary}";
            }

            return "Wystapił błąd podczas tworzenia nowego kalendarza. Spróbuj ponownie później";
        }

        private async Task<HttpResponseMessage> CreateCalendar(string calendarSummary)
        {
            var newOne = new NewCalendarDto
            {
                Summary = calendarSummary
            };

            return await this._httpClient.PostAsJsonAsync("calendars", newOne);
        }

        private async Task<HttpResponseMessage> CreateEvents(string calendarId, DateTime startDate, DateTime endDate)
        {
            var daysCount = (endDate.Day - startDate.Day) + 1;
            var newEvents = new EventsDto
            {
                Summary = "DevAdventCalendar",
                Location = @"https://devadventcalendar.pl/",
                Start = new StartDate
                {
                    DateTime = startDate,
                    TimeZone = @"Europe/Warsaw"
                },
                End = new EndDate
                {
                    DateTime = startDate.AddHours(1),
                    TimeZone = @"Europe/Warsaw"
                },
                Recurrence = new List<string>()
                {
                    $"RRULE:FREQ=DAILY;COUNT={daysCount}"
                }
            };

            return await this._httpClient.PostAsJsonAsync($"calendars/{calendarId}/events", newEvents);
        }
    }
}