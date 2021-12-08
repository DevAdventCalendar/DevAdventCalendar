using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DevAdventCalendarCompetition.Repository.Models;

namespace DevAdventCalendarCompetition.Services.Models
{
    public class TestDto
    {
        private string _description;

        public int Id { get; set; }

        public int Number { get; set; }

        public string Description
        {
            get => HtmlSanitizer.Sanitize(this._description);
            set => this._description = value;
        }

#pragma warning disable CA2227 // Collection properties should be read only
        public List<TestAnswerDto> Answers { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        public Uri PartnerLogoUrl { get; set; }

        public string PartnerName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public TestStatus Status { get; set; }

        public string Discount { get; set; }

        public Uri DiscountUrl { get; set; }

        public Uri DiscountLogoUrl { get; set; }

        public string DiscountLogoPath { get; set; }

        public bool HasUserAnswered { get; set; }

        public string UserAnswer { get; set; }
    }
}