using AutoMapper;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Profiles
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<Test, TestDto>()
                .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.HashedAnswer))
                .ForMember(dest => dest.HasUserAnswered, opt => opt.MapFrom(src => src.HashedAnswer));
            CreateMap<TestDto, Test>()
                .ForMember(dest => dest.Answers, opt => opt.Ignore())
                .ForMember(dest => dest.HashedAnswer, opt => opt.Ignore())
                .ForMember(dest => dest.WrongAnswers, opt => opt.Ignore());
            CreateMap<TestAnswer, TestAnswerDto>()
                .ForMember(dest => dest.AnsweringTime, opt => opt.MapFrom(src => src.AnsweringTime))
                .ForMember(dest => dest.AnsweringTimeOffset, opt => opt.MapFrom(src => src.AnsweringTimeOffset))
                .ForMember(dest => dest.TestId, opt => opt.MapFrom(src => src.TestId))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
            CreateMap<TestWrongAnswer, TestWrongAnswerDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.Answer))
                .ForMember(dest => dest.TestId, opt => opt.MapFrom(src => src.TestId))
                .ForMember(dest => dest.AnsweringTime, opt => opt.MapFrom(src => src.Time))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.Email));
            CreateMap<Test, TestWithAnswerListDto>()
                .ForMember(dest => dest.TestId, opt => opt.Ignore());

        }
    }
}