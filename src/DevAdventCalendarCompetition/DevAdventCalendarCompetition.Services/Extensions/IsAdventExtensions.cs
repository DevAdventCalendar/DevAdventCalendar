using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DevAdventCalendarCompetition.Services.Extensions
{
    public static class IsAdventExtensions
    {
        public static bool CheckIsAdvent(IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (Convert.ToDateTime(configuration.GetSection("Competition:StartDate"), CultureInfo.InvariantCulture) <= DateTime.Now &&
                Convert.ToDateTime(configuration.GetSection("Competition:EndDate"), CultureInfo.InvariantCulture) >= DateTime.Now)
            {
                return true;
            }

            return false;
        }
    }
}
