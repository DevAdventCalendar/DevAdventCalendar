using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IGoogleCalendarService
    {
       Task<CalendarList> GetAllCalendars();

       Task<string> CreateNewCalendarWithEvents(string calendarSummary, DateTime startDate, DateTime endDate);
    }
}
