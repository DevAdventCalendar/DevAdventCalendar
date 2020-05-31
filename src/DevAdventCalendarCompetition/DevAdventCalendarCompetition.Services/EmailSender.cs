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

        public string From { get; set; }

        public bool Ssl { get; set; }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mailMesage = this.CreateMailMessage(subject, message);
            mailMesage.From.Add(InternetAddress.Parse(this.From));
            mailMesage.To.Add(InternetAddress.Parse(email));

            using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
            {
                await smtpClient.ConnectAsync(this.Host, this.Port, this.Ssl);
                await smtpClient.AuthenticateAsync(this.UserName, this.Password);
                await smtpClient.SendAsync(mailMesage);
                await smtpClient.DisconnectAsync(true);
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
                Sender = MailboxAddress.Parse(this.From),
                Subject = subject,
                Body = bodyBuilder.ToMessageBody()
            };

            return message;
        }
    }
}