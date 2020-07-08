using System.Net;
using DevAdventCalendarCompetition.Resources;
using Microsoft.AspNetCore.Html;

namespace DevAdventCalendarCompetition.Providers
{
    public static class ErrorMessagesProvider
    {
        public static HtmlString GetMessageBody(int statusCode)
        {
            string value;
            switch ((HttpStatusCode)statusCode)
            {
                case HttpStatusCode.NotFound:
                    value = ErrorMessages.NotFound;
                    break;
                default:
                    value = ErrorMessages.Default;
                    break;
            }

            return new HtmlString(value);
        }
    }
}