using AutoMapper;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Profiles
{
    public class TestProfile : Profile
    {
        // disabled rule CA1506
        // technical debt
#pragma warning disable CA1506
        public TestProfile()
#pragma warning restore CA1506
        {
            this.CreateMap<TestAnswer, TestAnswerDto>()
                .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.Answer)).ReverseMap();
            this.CreateMap<Test, TestDto>()
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.HashedAnswers))
                .ForMember(dest => dest.PartnerName, opt => opt.MapFrom(src => src.SponsorName))
                .ForMember(dest => dest.PartnerLogoUrl, opt => opt.MapFrom(src => src.SponsorLogoUrl))
                .ForMember(dest => dest.HasUserAnswered, opt => opt.Ignore())
                .ForMember(dest => dest.UserAnswer, opt => opt.Ignore());
            this.CreateMap<TestDto, Test>()
                .ForMember(dest => dest.SponsorName, opt => opt.MapFrom(src => src.PartnerName))
                .ForMember(dest => dest.SponsorLogoUrl, opt => opt.MapFrom(src => src.PartnerLogoUrl))
                .ForMember(dest => dest.UserCorrectAnswers, opt => opt.Ignore())
                .ForMember(dest => dest.UserWrongAnswers, opt => opt.Ignore())
                .ForMember(dest => dest.HashedAnswers, opt => opt.Ignore());
            this.CreateMap<UserTestCorrectAnswer, UserTestCorrectAnswerDto>()
                .ForMember(dest => dest.AnsweringTime, opt => opt.MapFrom(src => src.AnsweringTime))
                .ForMember(dest => dest.AnsweringTimeOffset, opt => opt.MapFrom(src => src.AnsweringTimeOffset))
                .ForMember(dest => dest.TestId, opt => opt.MapFrom(src => src.TestId))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
            this.CreateMap<UserTestWrongAnswer, UserTestWrongAnswerDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.Answer))
                .ForMember(dest => dest.TestId, opt => opt.MapFrom(src => src.TestId))
                .ForMember(dest => dest.AnsweringTime, opt => opt.MapFrom(src => src.Time))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.Email));
            this.CreateMap<Test, TestWithUserCorrectAnswerListDto>()
                .ForMember(dest => dest.TestId, opt => opt.Ignore());
        }
    }
}