using System;

namespace DevAdventCalendarCompetition.Services.Options
{
    public class EmailNotificationOptions
    {
        public Uri SubscribeUrl { get; set; }

        public Uri UnsubscribeUrl { get; set; }

        public string ApiKey { get; set; }

        public string ListId { get; set; }
    }
}
