using System;
using System.Linq;
using AutoFixture;
using AutoFixture.Xunit2;

namespace DevAdventCalendarCompetition.Tests.AutoFixture
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestWithNumberWithoutAnswersDataAttribute : AutoDataAttribute
    {
        public TestWithNumberWithoutAnswersDataAttribute()
            : base(() =>
            {
                var fixture = new Fixture();
                fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => fixture.Behaviors.Remove(b));
                fixture.Behaviors.Add(new OmitOnRecursionBehavior());
                fixture.Customizations.Add(new TestNumberSpecimen());
                fixture.Customizations.Add(new TestWithoutAnswersSpecimen());
                return fixture;
            })
        {
        }
    }
}