using System;
using System.Collections.Generic;
using System.Globalization;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.Services.Models
{
    public class TestDto
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public string Description { get; set; }

#pragma warning disable CA2227 // Collection properties should be read only
        public List<TestAnswerDto> Answers { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        public Uri SponsorLogoUrl { get; set; }

        public string SponsorName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public TestStatus Status { get; set; }

        public string Discount { get; set; }

        public Uri DiscountUrl { get; set; }

        public Uri DiscountLogoUrl { get; set; }

        public string DiscountLogoPath { get; set; }

        public bool HasUserAnswered { get; set; }

        /*
         * public static void SetIsAdvent(TestDto testDto, string defaultDateTimeFormat = "dd-MM-yyyy")
        {
            if (testDto == null)
            {
                throw new ArgumentNullException(nameof(testDto));
            }

#pragma warning disable CA1062 // Validate arguments of public methods
            var isAdventEndDate = configuration.GetSection("Competition:EndDate").Value;
#pragma warning restore CA1062 // Validate arguments of public methods
            var isAdventStartDate = configuration.GetSection("Competition:StartDate").Value;
            var correctStartDateTimeFormat = DateTimeOffset.TryParseExact(isAdventStartDate, defaultDateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var dateValue);
            var correctEndDateTimeFormat = DateTimeOffset.TryParseExact(isAdventEndDate, defaultDateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var dateValue1);

            if (!correctStartDateTimeFormat || correctEndDateTimeFormat)
            {
                throw new InvalidOperationException(
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                    $"Niepoprawny format daty zmiennej ResultPublicationDateTime w appsettings ({defaultDateTimeFormat})");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            }

            if (Convert.ToDateTime(isAdventStartDate, CultureInfo.InvariantCulture) >= DateTime.Now
                && Convert.ToDateTime(isAdventEndDate, CultureInfo.InvariantCulture) <= DateTime.Now)
            {
                testDto.IsAdvent = true;

                return;
            }

            testDto.IsAdvent = false;
        }*/
    }
}