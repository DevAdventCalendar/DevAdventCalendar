using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Services.Interfaces;
using MimeKit;

namespace DevAdventCalendarCompetition.Services
{
    public class EmailSender : IEmailSender
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FromEmail { get; set; }

        public string FromName { get; set; }

        public bool Ssl { get; set; }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            if (!string.IsNullOrWhiteSpace(this.UserName) && !string.IsNullOrWhiteSpace(this.Password))
            {
                var mailMesage = this.CreateMailMessage(subject, message);
                mailMesage.From.Add(InternetAddress.Parse(this.FromEmail));
                mailMesage.To.Add(InternetAddress.Parse(email));

                using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
                {
                    await smtpClient.ConnectAsync(this.Host, this.Port, true).ConfigureAwait(false);
                    await smtpClient.AuthenticateAsync(this.UserName, this.Password).ConfigureAwait(false);
                    await smtpClient.SendAsync(mailMesage).ConfigureAwait(false);
                    await smtpClient.DisconnectAsync(true).ConfigureAwait(false);
                }
            }
        }

        private MimeMessage CreateMailMessage(string subject, string body)
        {
            var bodyBuilder = new MimeKit.BodyBuilder
            {
                HtmlBody = body
            };

            var message = new MimeMessage
            {
                Sender = new MailboxAddress(this.FromName, this.FromEmail),
                Subject = subject,
                Body = bodyBuilder.ToMessageBody()
            };

            return message;
        }
    }
}