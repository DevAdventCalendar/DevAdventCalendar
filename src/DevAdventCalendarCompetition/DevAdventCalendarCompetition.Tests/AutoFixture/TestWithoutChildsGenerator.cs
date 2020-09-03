using System;
using System.Reflection;
using AutoFixture.Kernel;

namespace DevAdventCalendarCompetition.Tests.AutoFixture
{
    public class TestWithoutChildsGenerator : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            var propertyInfo = request as PropertyInfo;

            if (propertyInfo is null)
            {
                return new NoSpecimen();
            }

            var isUCAProperty = propertyInfo.Name.Contains("UserCorrectAnswers", StringComparison.InvariantCulture);
            var isUWAProperty = propertyInfo.Name.Contains("UserWrongAnswers", StringComparison.InvariantCulture);

            if (isUCAProperty || isUWAProperty)
            {
                return null;
            }

            return new NoSpecimen();
        }
    }
}