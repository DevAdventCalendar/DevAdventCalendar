using System;
using AutoMapper;
using DevAdventCalendarCompetition.Services.Profiles;
using Xunit;

namespace DevAdventCalendarCompetition.Tests.UnitTests
{
    public class MapperTest
    {
        [Fact]
        public void TestProfileMappingsIsValid()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TestProfile>();
            });
            var mapper = mockMapper.CreateMapper();

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void TestAnswerProfileIsValid()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TestAnswerProfile>();
            });
            var mapper = mockMapper.CreateMapper();

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}