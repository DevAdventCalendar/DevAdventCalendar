using AutoMapper;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Profiles
{
    public class TestAnswerProfile : Profile
    {
        public TestAnswerProfile()
        {
            this.CreateMap<TestAnswer, TestAnswerDto>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.Email));

            this.CreateMap<TestAnswerDto, TestAnswer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Test, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            this.CreateMap<ApplicationUser, TestAnswer>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Test, opt => opt.Ignore())
                .ForMember(dest => dest.TestId, opt => opt.Ignore())
                .ForMember(dest => dest.AnsweringTime, opt => opt.Ignore())
                .ForMember(dest => dest.AnsweringTimeOffset, opt => opt.Ignore());

            this.CreateMap<Test, TestAnswer>()
                .ForMember(dest => dest.Test, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.TestId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.AnsweringTime, opt => opt.Ignore())
                .ForMember(dest => dest.AnsweringTimeOffset, opt => opt.Ignore());
        }
    }
}