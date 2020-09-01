using System;
using System.Collections.Generic;
using System.Text;

namespace DevAdventCalendarCompetition.Services
{
    public static class DateTimeProvider
    {
        public static DateTime Now
            => DateTimeProviderContext.Current == null
                ? DateTime.Now
                : DateTimeProviderContext.Current.ContextDateTimeNow;
    }
}
