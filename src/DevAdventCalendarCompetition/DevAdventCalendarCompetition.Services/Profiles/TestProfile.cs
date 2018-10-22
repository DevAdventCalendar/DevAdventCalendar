using AutoMapper;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Profiles
{
	internal class TestProfile : Profile
	{
		public TestProfile()
		{
			CreateMap<Test, TestDto>();
			CreateMap<TestDto, Test>();
		}
	}
}
