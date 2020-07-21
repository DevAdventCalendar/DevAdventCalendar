using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper.Configuration;
using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevAdventCalendarCompetition.Services.Models
{
    public class TestDto
    {
        public const string DefaultDateTimeFormat = "yyyy-MM-dd";

        public bool IsAdvent { get;  private set; }

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

        public static void SetIsAdvent(Microsoft.Extensions.Configuration.IConfiguration configuration, TestDto testDto)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            var isAdventEndDate = configuration.GetSection("Competition:EndDate").Value;
#pragma warning restore CA1062 // Validate arguments of public methods
            var isAdventStartDate = configuration.GetSection("Competition:StartDate").Value;
            var correctStartDateTimeFormat = DateTimeOffset.TryParseExact(isAdventStartDate, DefaultDateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var dateValue);
            var correctEndDateTimeFormat = DateTimeOffset.TryParseExact(isAdventEndDate, DefaultDateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var dateValue1);

            CultureInfo culture = CultureInfo.InvariantCulture;

            if (!correctStartDateTimeFormat || correctEndDateTimeFormat)
            {
                throw new InvalidOperationException(
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                    $"Niepoprawny format daty zmiennej ResultPublicationDateTime w appsettings ({DefaultDateTimeFormat})");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            }

            if (Convert.ToDateTime(isAdventStartDate, culture) >= DateTime.Now && Convert.ToDateTime(isAdventEndDate, culture) <= DateTime.Now)
            {
#pragma warning disable CA1062 // Validate arguments of public methods
                testDto.IsAdvent = true;
#pragma warning restore CA1062 // Validate arguments of public methods
                return;
            }

            testDto.IsAdvent = false;
            return;
        }

        /*
        private static void ValidateResultPublicationDateTime(string resultPublicationDateTimeValue)
        {
            var correctDateTimeFormat = DateTimeOffset.TryParseExact(resultPublicationDateTimeValue, DefaultDateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var dateValue);
            if (!correctDateTimeFormat)
            {
                throw new InvalidOperationException(
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                    $"Niepoprawny format daty zmiennej ResultPublicationDateTime w appsettings ({DefaultDateTimeFormat})");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            }
        }*/
    }
}