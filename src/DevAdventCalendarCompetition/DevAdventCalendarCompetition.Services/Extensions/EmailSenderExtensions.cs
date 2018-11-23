using DevAdventCalendarCompetition.Services.Interfaces;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DevAdventCalendarCompetition.Services.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Potwierdzenie za�o�enia konta", 
                $"Prosimy o potwierdzenie za�o�enia konta poprzez klikni�cie na link" +
                $"<p><a href='{ link }'>LINK</a></p><p>Pozdrawiamy,<br />Elfy DevAdventCalendar</p>" +
                $"<p>PS. Dodatkowo b�dzie nam bardzo mi�o, je�li dasz �apk� w g�r� na <a href='https://www.facebook.com/devadventcalendar/'>Facebooku</a> " +
                $"i <a href='https://twitter.com/dev_advent_cal'>Twitterze</a>!</p>");
        }
    }
}