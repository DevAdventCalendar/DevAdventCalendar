using System.Threading.Tasks;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}