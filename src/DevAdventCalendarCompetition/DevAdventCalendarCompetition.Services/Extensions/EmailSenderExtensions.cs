using DevAdventCalendarCompetition.Services.Interfaces;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DevAdventCalendarCompetition.Services.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Potwierdzenie za³o¿enia konta", 
                $"Prosimy o potwierdzenie za³o¿enia konta poprzez klikniêcie na link" +
                $"<p><a href='{ link }'>LINK</a></p><p>Pozdrawiamy,<br />Elfy DevAdventCalendar</p>" +
                $"<p>PS. Dodatkowo bêdzie nam bardzo mi³o, jeœli dasz ³apkê w górê na <a href='https://www.facebook.com/devadventcalendar/'>Facebooku</a> " +
                $"i <a href='https://twitter.com/dev_advent_cal'>Twitterze</a>!</p>");
        }
    }
}