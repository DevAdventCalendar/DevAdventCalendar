using System.Threading.Tasks;

namespace DevAdventCalendarCompetition.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}