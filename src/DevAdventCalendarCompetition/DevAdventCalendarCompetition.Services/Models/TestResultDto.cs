using System;
using System.Collections.Generic;
using System.Text;

namespace DevAdventCalendarCompetition.Services.Models
{
    public class TestResultDto
    {
        public string UserId { get; set; }

        public string Email { get; set; }

        public int CorrectAnswersCount { get; set; }
   
        public int WrongAnswersCount { get; set; }
       
        public int Points { get; set; }

        public int Position { get; set; }
    }
}