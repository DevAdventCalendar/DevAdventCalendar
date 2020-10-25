using System.Globalization;
using System.Text.RegularExpressions;

namespace DevAdventCalendarCompetition.Services
{
    public class AnswerService
    {
#pragma warning disable CA1822 // Mark members as static
        public string ParseUserAnswer(string userAnswer)
#pragma warning restore CA1822 // Mark members as static
        {
            return string.IsNullOrWhiteSpace(userAnswer) ?
                string.Empty :
                Regex.Replace(userAnswer.ToUpper(CultureInfo.CurrentCulture).Trim(), "\\s{2,}", " ");
        }
    }
}
