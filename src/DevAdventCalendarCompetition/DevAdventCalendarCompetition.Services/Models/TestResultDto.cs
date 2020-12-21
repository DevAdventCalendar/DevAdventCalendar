namespace DevAdventCalendarCompetition.Services.Models
{
    public class TestResultDto
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public int CorrectAnswersCount { get; set; }

        public int WrongAnswersCount { get; set; }

        public double TotalTime { get; set; }

        public int Points { get; set; }

        public int Position { get; set; }
    }
}