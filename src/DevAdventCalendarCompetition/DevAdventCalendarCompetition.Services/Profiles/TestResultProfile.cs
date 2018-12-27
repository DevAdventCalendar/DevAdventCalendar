using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Profiles
{
    public class TestResultProfile : Profile
    {
        public TestResultProfile()
        {
            CreateMap<Result, TestResultDto>()
                .ForMember(d => d.Email, opt => opt.MapFrom(src => src.User.Email));
        }
    }
}
