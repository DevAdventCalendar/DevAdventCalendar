using System;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoMapper;
using DevAdventCalendarCompetition.Services.Profiles;

namespace DevAdventCalendarCompetition.Tests.AutoFixture
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestWithNumberDataAttribute : AutoDataAttribute
    {
        public TestWithNumberDataAttribute()
            : base(() =>
            {
                var fixture = new Fixture().Customize(new AutoMoqCustomization());
                fixture.Register<IMapper>(() =>
                    new MapperConfiguration(cfg => cfg.AddMaps(typeof(TestProfile))).CreateMapper());
                fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => fixture.Behaviors.Remove(b));
                fixture.Behaviors.Add(new OmitOnRecursionBehavior());
                fixture.Customizations.Add(new TestNumberSpecimen());
                return fixture;
            })
        {
        }
    }
}