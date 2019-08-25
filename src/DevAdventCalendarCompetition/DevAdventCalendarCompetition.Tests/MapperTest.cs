using System;
using AutoMapper;
using DevAdventCalendarCompetition.Services.Profiles;
using Xunit;

namespace DevAdventCalendarCompetition.Tests
{
    public class MapperTest
    {
        [Fact]
#pragma warning disable CA1041 // Provide ObsoleteAttribute message
        [Obsolete]
#pragma warning restore CA1041 // Provide ObsoleteAttribute message
        public void TestProfileMappingsIsValid()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<TestProfile>();
            });

            Mapper.Configuration.AssertConfigurationIsValid();
        }

        [Fact]
#pragma warning disable CA1041 // Provide ObsoleteAttribute message
        [Obsolete]
#pragma warning restore CA1041 // Provide ObsoleteAttribute message
        public void TestAnswerProfileIsValid()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<TestAnswerProfile>();
            });

            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}