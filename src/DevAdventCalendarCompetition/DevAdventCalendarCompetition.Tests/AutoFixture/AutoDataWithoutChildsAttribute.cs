using System;
using System.Linq;
using AutoFixture;
using AutoFixture.Xunit2;

namespace DevAdventCalendarCompetition.Tests.AutoFixture
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AutoDataWithoutChildsAttribute : AutoDataAttribute
    {
        public AutoDataWithoutChildsAttribute()
            : base(() =>
            {
                var fixture = new Fixture();
                fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => fixture.Behaviors.Remove(b));
                fixture.Behaviors.Add(new OmitOnRecursionBehavior());
                fixture.Customizations.Add(new TestNumberGenerator());
                fixture.Customizations.Add(new TestWithoutChildsGenerator());
                return fixture;
            })
        {
        }
    }
}