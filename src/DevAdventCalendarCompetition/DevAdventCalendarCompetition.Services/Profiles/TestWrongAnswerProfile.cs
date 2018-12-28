using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Profiles
{
    public class TestWrongAnswerProfile : Profile
    {
        public TestWrongAnswerProfile()
        {
            CreateMap<Test, TestWrongAnswer>()
                .ForMember(dest => dest.Test, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.TestId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Time, opt => opt.Ignore())
                .ForMember(dest => dest.Answer, opt => opt.Ignore());

            CreateMap<TestWrongAnswer, TestWrongAnswerDto>()
                .ForMember(dest => dest.AnsweringTime, opt => opt.MapFrom(src => src.Time));
        }
    }
}
