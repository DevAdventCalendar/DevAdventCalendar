using DevAdventCalendarCompetition.Services.Options;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DevAdventCalendarCompetition.TestResultService
{
    public static class TestSettingsFactory
    {
        public static TestSettings LoadTestSettings()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile($"appsetins.json", optional: false);

            var configuration = configurationBuilder.Build();

            return new TestSettings()
            {
                StartHour = configuration.GetValue<TimeSpan>("Test:StartHour"),
                EndHour = configuration.GetValue<TimeSpan>("Test:EndHour")
            };
        }
    }
}
