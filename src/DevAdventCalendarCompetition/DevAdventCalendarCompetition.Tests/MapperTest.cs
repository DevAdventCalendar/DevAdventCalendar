using AutoMapper;
using DevAdventCalendarCompetition.Services.Profiles;
using Xunit;

namespace DevAdventCalendarCompetition.Tests
{
    public class MapperTest
    {
        [Fact]
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