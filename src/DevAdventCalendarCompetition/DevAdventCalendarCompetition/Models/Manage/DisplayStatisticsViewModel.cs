using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevAdventCalendarCompetition.Models.Manage
{
    public class DisplayStatisticsViewModel
    {
        // not used for now
        public int WrongAnswerCount { get; set; }

        // not used for now
        public DateTime CorrectAnswerDateTime { get; set; }

        // not used for now
        public string StatusMessage { get; set; }

        public string WrongAnswerCountMessage { get; set; }

        public string CorrectAnswerDateMessage { get; set; }
    }
}