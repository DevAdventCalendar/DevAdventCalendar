using System;
using System.Linq;
using DevAdventCalendarCompetition.Repository;
using Xunit;

namespace DevAdventCalendarCompetition.TestResultService.Tests
{
    [Collection(nameof(UserTestAnswersRepositoryCollection))]
    public class UserTestAnswersRepositoryTest
    {
        private readonly UserTestAnswersRepository _userTestAnswersRepository;

        public UserTestAnswersRepositoryTest(UserTestAnswersRepositoryFixture fixture)
        {
            _userTestAnswersRepository = fixture.GetUserTestAnswersRepository();
        }

        [Fact]
        public void GetCorrectAnswersPerUserForDateRange__Returns_1__When_ManyCorrectAnswerForTheSameTest()
        {
            //Act
            var results = _userTestAnswersRepository.GetCorrectAnswersPerUserForDateRange(
                new DateTime(2019, 12, 1, 20, 0, 0),
                new DateTime(2019, 12, 24, 23, 59, 0)
            );

            //Assert
            Assert.Equal(1, results.Keys.Count);
            Assert.Equal(1, results.Values.First());
        }

        [Fact]
        public void GetAnsweringTimeSumPerUserForDateRange__Returns_TheFastestCorrectAnswerTime__When_ManyCorrectAnswerForTheSameTest()
        {
            //Act
            var results = _userTestAnswersRepository.GetAnsweringTimeSumPerUserForDateRange(
                new DateTime(2019, 12, 1, 20, 0, 0),
                new DateTime(2019, 12, 24, 23, 59, 0)
            );

            //Assert
            Assert.Equal(1, results.Keys.Count);
            Assert.Equal((new TimeSpan(0, 2, 0, 4)).TotalSeconds, results.Values.First());
        }
    }
}