using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Services.Interfaces;

namespace DevAdventCalendarCompetition.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            using (var smtpClient = new SmtpClient
            {
                Host = this.Host,
                Port = this.Port,
                EnableSsl = true,
                Credentials = new NetworkCredential(this.UserName, this.Password)
            })

            using (var mailMessage = new MailMessage("devadventcalendar@gmail.com", email)
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = message
            })
            {
                return smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}