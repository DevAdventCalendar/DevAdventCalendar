using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Options;

namespace DevAdventCalendarCompetition.Services
{
    public class AdventService : IAdventService
    {
        private readonly AdventSettings _adventSettings;
        private readonly IDateTimeService _dateTimeService;

        public AdventService(AdventSettings adventSettings, IDateTimeService dateTimeService)
        {
            this._adventSettings = adventSettings;
            this._dateTimeService = dateTimeService;
        }

        public bool IsAdvent()
        {
            var now = this._dateTimeService.Now;
            return this._adventSettings.StartDate <= now && this._adventSettings.EndDate.AddDays(1) >= now;
        }
    }
}
