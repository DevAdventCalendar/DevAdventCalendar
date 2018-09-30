using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevAdventCalendarCompetition.Vms
{
    public class SingleTestResultEntry
    {
        public string FullName { get; set; }

        public TimeSpan Offset { get; set; }

        public int Points { get; set; }

        public string UserId { get; set; }
    }
}