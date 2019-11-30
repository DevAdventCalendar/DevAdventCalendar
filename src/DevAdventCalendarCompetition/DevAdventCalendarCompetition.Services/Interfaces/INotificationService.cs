using System.Threading.Tasks;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface INotificationService
    {
        Task<bool> SetSubscriptionPreferenceAsync(string email, bool subscribe);
    }
}
