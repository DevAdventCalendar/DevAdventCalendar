using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Services.Interfaces;

namespace DevAdventCalendarCompetition.Services
{
    public class EmailSender : IEmailSender
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string From { get; set; }

        public bool Ssl { get; set; }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using (var smtpClient = new SmtpClient(this.Host, this.Port) { EnableSsl = this.Ssl })
            {
                if (!string.IsNullOrWhiteSpace(this.UserName) && !string.IsNullOrWhiteSpace(this.Password))
                {
                    smtpClient.Credentials = new NetworkCredential(this.UserName, this.Password);
                }

                using (var mailMessage = new MailMessage(this.From, email, subject, message) { IsBodyHtml = true })
                {
                    await smtpClient.SendMailAsync(mailMessage).ConfigureAwait(false);
                }
            }
        }
    }
}