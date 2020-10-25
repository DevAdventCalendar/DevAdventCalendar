using System.Globalization;
using System.Text.RegularExpressions;
using DevAdventCalendarCompetition.Services.Interfaces;

namespace DevAdventCalendarCompetition.Services
{
    public class AnswerService : IAnswerService
    {
        public string ParseUserAnswer(string answer)
        {
            return string.IsNullOrWhiteSpace(answer) ?
                string.Empty :
                Regex.Replace(answer.ToUpper(CultureInfo.CurrentCulture).Trim(), "\\s{2,}", " ");
        }
    }
}
