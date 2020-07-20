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

        /*
#pragma warning disable CA1822
        public bool IsAdvent => true; // DateTime.Now.Month == 7 && DateTime.Now.Day < 25;
#pragma warning restore CA1822
        */
        public static bool IsAdvent => SetIsAdvent();

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

        private static bool SetIsAdvent(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            var isAdventEndDate = configuration.GetSection("Competition:EndDate").Value;
            var isAdventStartDate = configuration.GetSection("Competition:StartDate").Value;
            ValidateResultPublicationDateTime(isAdventEndDate);
            ValidateResultPublicationDateTime(isAdventStartDate);

            CultureInfo culture = new CultureInfo("en-Us");

            if (Convert.ToDateTime(isAdventStartDate, culture) >= DateTime.Now && Convert.ToDateTime(isAdventEndDate, culture) <= DateTime.Now)
            {
                return true;
            }

            return false;
        }

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
        }
    }
}