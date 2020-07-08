using AutoMapper;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Profiles
{
    public class TestWrongAnswerProfile : Profile
    {
        public TestWrongAnswerProfile()
        {
            this.CreateMap<Test, UserTestWrongAnswer>()
                .ForMember(dest => dest.Test, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.TestId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Time, opt => opt.Ignore())
                .ForMember(dest => dest.Answer, opt => opt.Ignore());

            this.CreateMap<UserTestWrongAnswer, UserTestWrongAnswerDto>()
                .ForMember(dest => dest.AnsweringTime, opt => opt.MapFrom(src => src.Time))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User == null ? "Użytkownik Anonimowy" : src.User.Email));
        }
    }
}