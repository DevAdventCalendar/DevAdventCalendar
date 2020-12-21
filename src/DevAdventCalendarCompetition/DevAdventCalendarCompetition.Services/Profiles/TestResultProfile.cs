using AutoMapper;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Profiles
{
    public class TestResultProfile : Profile
    {
        public TestResultProfile()
        {
            this.CreateMap<Result, TestResultDto>()
                .ForMember(
                    d => d.UserName,
                    opt => opt.MapFrom(src => src.User == null ? "Użytkownik Anonimowy" : src.User.UserName));
        }
    }
}