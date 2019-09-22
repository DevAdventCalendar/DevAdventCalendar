using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Services.Interfaces;

namespace DevAdventCalendarCompetition.Services.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            if (emailSender is null)
            {
                throw new System.ArgumentNullException(nameof(emailSender));
            }

            return emailSender.SendEmailAsync(email, "Potwierdzenie założenia konta", $"{$"Prosimy o potwierdzenie założenia konta poprzez kliknięcie na link <p><a href='{link}'>LINK</a></p>"}{$"<p>Jeżeli autentykacja została przeprowadzona przy pomocy zewnętrznego dostawcy (Facebook, Twitter, Google, GitHub), "}{$"żeby się zalogować do kalendarza, po przejściu na ekran logowania należy kliknąć w przycisk odpowiadający danemu dostawcy.</p>"}{$"<p>Pozdrawiamy,<br />Elfy DevAdventCalendar</p>"}{$"<p>PS. Dodatkowo będzie nam bardzo miło, jeśli dasz łapkę w górę na <a href='https://www.facebook.com/devadventcalendar/'>Facebooku</a> "}{$"i <a href='https://twitter.com/dev_advent_cal'>Twitterze</a>!</p>"}");
        }
    }
}