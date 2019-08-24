using System;
using AutoMapper;
using DevAdventCalendarCompetition.Services.Profiles;
using Xunit;

namespace DevAdventCalendarCompetition.Tests
{
    public class MapperTest
    {
        [Fact]
        [Obsolete]
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
        [Obsolete]
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