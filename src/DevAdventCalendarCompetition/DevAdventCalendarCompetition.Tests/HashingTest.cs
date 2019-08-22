using System.Security.Cryptography;
using DevAdventCalendarCompetition.Services;
using Xunit;

namespace DevAdventCalendarCompetition.Tests
{
    public class HashingTest
    {
        private const string PASSWORD = "P@ssw0rd";
        private const int SALTLENGTH = 32;
        private readonly StringHasher _stringHasher;
        private readonly HashParameters _hashParameters;

        public HashingTest()
        {
            this._hashParameters = new HashParameters(10, this.GenerateSalt());
            this._stringHasher = new StringHasher(this._hashParameters);
        }

        [Theory]
        [InlineData("P@ssw0rd")]
        public void AnswerIsValid(string value)
        {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
            string passwordHash = this._stringHasher.ComputeHash(PASSWORD);
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            this._stringHasher.VerifyHash(value, passwordHash);
        }

        [Theory]
        [InlineData("p@ssw0rd")]
        [InlineData("passw0rd")]
        [InlineData("password")]
        [InlineData("p@ssw0rD")]
        public void AnswerIsInvalid(string value)
        {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
            string passwordHash = this._stringHasher.ComputeHash(PASSWORD);
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            this._stringHasher.VerifyHash(value, passwordHash);
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[SALTLENGTH];

            using (RandomNumberGenerator random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }
    }
}