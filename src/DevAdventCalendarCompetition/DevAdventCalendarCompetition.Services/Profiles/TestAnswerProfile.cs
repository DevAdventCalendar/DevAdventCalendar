using AutoMapper;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services.Profiles
{
	internal class TestAnswerProfile : Profile
	{
		public TestAnswerProfile()
		{
			CreateMap<TestAnswer, TestAnswerDto>();
			CreateMap<TestAnswerDto, TestAnswer>();
		}
	}
}
