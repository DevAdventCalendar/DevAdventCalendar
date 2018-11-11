using DevAdventCalendarCompetition.Services;
using System.Security.Cryptography;
using Xunit;

namespace DevAdventCalendarCompetition.Tests
{
    public class HashingTest
    {
        private const string PASSWORD = "P@ssw0rd";
        private const int SALT_LENGTH = 32;
        private readonly StringHasher _stringHasher;
        private readonly HashParameters _hashParameters;

        public HashingTest()
        {
            _hashParameters = new HashParameters(10, GenerateSalt());
            _stringHasher = new StringHasher(_hashParameters);
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[SALT_LENGTH];

            using (RandomNumberGenerator random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }

        [Theory]
        [InlineData("P@ssw0rd")]
        public void AnswerIsValid(string value)
        {
            string passwordHash = _stringHasher.ComputeHash(PASSWORD);
            _stringHasher.VerifyHash(value, passwordHash);
        }

        [Theory]
        [InlineData("p@ssw0rd")]
        [InlineData("passw0rd")]
        [InlineData("password")]
        [InlineData("p@ssw0rD")]
        public void AnswerIsInvalid(string value)
        {
            string passwordHash = _stringHasher.ComputeHash(PASSWORD);
            _stringHasher.VerifyHash(value, passwordHash);
        }
    }
}