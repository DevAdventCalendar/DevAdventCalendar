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
            // z konfiguracji pobrać start date i end date i porównwać z dzisiejszym dzisiaj w przedziale od start do end a
            // jesli nie to nie trwa i powinno być zwrócone fales
#pragma warning disable CA1062 // Validate arguments of public methods
            if (Convert.ToDateTime(configuration.GetSection("Competition:StartDate"), CultureInfo.InvariantCulture) <= DateTime.Now &&
                Convert.ToDateTime(configuration.GetSection("Competition:EndDate"), CultureInfo.InvariantCulture) >= DateTime.Now)
#pragma warning restore CA1062 // Validate arguments of public methods
            {
                return true;
            }

            return false;
        }
    }
}
