using System;
using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Models.Manage
{
    public class DisplayStatisticsViewModel
    {
        public int WrongAnswerCount { get; set; }

        public DateTime CorrectAnswerDateTime { get; set; }

        public string MessageToUser { get; set; }

        public string StatusMessage { get; set; }
    }
}