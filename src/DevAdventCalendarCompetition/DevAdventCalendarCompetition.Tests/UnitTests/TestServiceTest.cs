using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AutoMapper;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Profiles;
using FluentAssertions;
using Moq;
using Xunit;

namespace DevAdventCalendarCompetition.Tests.UnitTests
{
    public class TestServiceTest
    {
        private readonly Mock<ITestRepository> _testRepositoryMock;
        private readonly Mock<IUserTestAnswersRepository> _testAnswerRepositoryMock;
        private IMapper _mapper;
        private StringHasher _hasher;

        public TestServiceTest()
        {
            this._testRepositoryMock = new Mock<ITestRepository>();
            this._testAnswerRepositoryMock = new Mock<IUserTestAnswersRepository>();
            this._mapper = new MapperConfiguration(cfg => cfg.AddMaps(typeof(TestProfile))).CreateMapper();
            this._hasher = new StringHasher(new HashParameters(100, new byte[] { 1, 2 }));
        }

        public static List<object[]> TestAnswerData()
        {
            return new List<object[]>
            {
                new object[] { "TEST1", new List<string> { @"UZjbt/sTWOV87jfKfV8ILWMu16fZ1V8+5QSMaFdzdv4=", @"/UWjjOu5zQXwoc8SC/LlKacbaFS7KcNLa/gG2a6UzTc=", @"mkmG0WTkcJWI+h9B6y5BzoUvbInPSyjfzbUNZSLjEFw=" }, true },
                new object[] { "TEST2", new List<string> { @"UZjbt/sTWOV87jfKfV8ILWMu16fZ1V8+5QSMaFdzdv4=", @"/UWjjOu5zQXwoc8SC/LlKacbaFS7KcNLa/gG2a6UzTc=", @"mkmG0WTkcJWI+h9B6y5BzoUvbInPSyjfzbUNZSLjEFw=" }, true },
                new object[] { "TEST3", new List<string> { @"UZjbt/sTWOV87jfKfV8ILWMu16fZ1V8+5QSMaFdzdv4=", @"/UWjjOu5zQXwoc8SC/LlKacbaFS7KcNLa/gG2a6UzTc=", @"mkmG0WTkcJWI+h9B6y5BzoUvbInPSyjfzbUNZSLjEFw=" }, true },
                new object[] { "test1", new List<string> { @"UZjbt/sTWOV87jfKfV8ILWMu16fZ1V8+5QSMaFdzdv4=", @"/UWjjOu5zQXwoc8SC/LlKacbaFS7KcNLa/gG2a6UzTc=", @"mkmG0WTkcJWI+h9B6y5BzoUvbInPSyjfzbUNZSLjEFw=" }, false },
                new object[] { "hi21", new List<string> { @"UZjbt/sTWOV87jfKfV8ILWMu16fZ1V8+5QSMaFdzdv4=", @"/UWjjOu5zQXwoc8SC/LlKacbaFS7KcNLa/gG2a6UzTc=", @"mkmG0WTkcJWI+h9B6y5BzoUvbInPSyjfzbUNZSLjEFw=" }, false }
            };
        }

        [Theory]
        [MemberData(nameof(TestAnswerData))]
        public void VerifyTestAnswer_WithAnswer_ShouldBeAsExpected(string userAnswer, List<string> correctAnswers, bool expected)
        {
            var sut = new TestService(this._testRepositoryMock.Object, this._testAnswerRepositoryMock.Object, this._mapper, this._hasher);

            var result = sut.VerifyTestAnswer(userAnswer, correctAnswers);

            result.Should().Be(expected);
        }
    }
}
