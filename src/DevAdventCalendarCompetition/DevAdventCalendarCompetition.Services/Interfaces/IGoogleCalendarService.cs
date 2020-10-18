using System.Threading.Tasks;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Interfaces
{
    public interface IGoogleCalendarService
    {
       Task<OperationalResult> CreateNewCalendarWithEvents();
    }
}