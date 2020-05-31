using System.Threading.Tasks;
using DevAdventCalendarCompetition.Services.Interfaces;

namespace DevAdventCalendarCompetition.Services.Extensions
{
    public static class EmailSenderExtensions
    {
        public static async Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            if (emailSender is null)
            {
                throw new System.ArgumentNullException(nameof(emailSender));
            }

            await emailSender
                .SendEmailAsync(email, "Potwierdzenie założenia konta", string.Concat(
                    $"{$"Prosimy o potwierdzenie założenia konta poprzez kliknięcie na link <p><a href='{link}'>LINK</a></p>"}",
                    $"{$"<p>Jeżeli autentykacja została przeprowadzona za pomocą zewnętrznego dostawcy (Facebook, Twitter, Google, GitHub), "}",
                    $"{$"po przejściu na ekran logowania należy kliknąć w przycisk odpowiadający danemu dostawcy.</p>"}",
                    $"{$"<p>W zakładce \"Moje konto\" możesz włączyć opcję \"Chcę otrzymywać notyfikacje email\" w celu otrzymania wiadomości email przypominającej o otwarciu okienka.</p>"}",
                    $"{$"<p>Pozdrawiamy,<br />Elfy DevAdventCalendar</p>"}",
                    $"{$"<p>PS. Dodatkowo będzie nam bardzo miło, jeśli dasz łapkę w górę na <a href='https://www.facebook.com/devadventcalendar/'>Facebooku</a> "}",
                    $"{$"i <a href='https://twitter.com/dev_advent_cal'>Twitterze</a>!</p>"}"))
                .ConfigureAwait(false);
        }
    }
}