using System;

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
