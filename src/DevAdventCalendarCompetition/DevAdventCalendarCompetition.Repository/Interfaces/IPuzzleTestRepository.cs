using System;
using System.Collections.Generic;
using System.Text;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.Repository.Interfaces
{
    public interface IPuzzleTestRepository
    {
        TestAnswer GetEmptyAnswerForStartedTestByUser(string userId);
    }
}
