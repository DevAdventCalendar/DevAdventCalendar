using System;
using System.Net;
using System.Net.Mail;
using DevAdventCalendarCompetition.Services.Interfaces;
using System.Threading.Tasks;

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
            var smtpClient = new SmtpClient
            {
                Host = Host,
                Port = Port,
                EnableSsl = true,
                //Credentials = new NetworkCredential("account_email_to_change", "password_to_change")
                Credentials = new NetworkCredential(UserName, Password)
            };

            var mailMessage = new MailMessage("devadventcalendar@gmail.com", email)
            {
                Subject = subject,
                Body = message
            };
            
            return smtpClient.SendMailAsync(mailMessage); 
        }
    }
}