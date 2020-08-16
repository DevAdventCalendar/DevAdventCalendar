using System;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace DevAdventCalendarCompetition.Services.Extensions
{
    public static class IsAdventExtensions
    {
        private const string DefaultDateTimeFormat = "dd-MM-yyyy";

        public static bool CheckIsAdvent(IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var startDate = configuration.GetSection("Competition:StartDate");
            var endDate = configuration.GetSection("Competition:EndDate");

            if (DateTimeOffset.ParseExact(startDate.Value, DefaultDateTimeFormat, CultureInfo.InvariantCulture) <= DateTime.Now &&
                DateTimeOffset.ParseExact(endDate.Value, DefaultDateTimeFormat, CultureInfo.InvariantCulture) >= DateTime.Now)
            {
                return true;
            }

            return false;
        }
    }
}
