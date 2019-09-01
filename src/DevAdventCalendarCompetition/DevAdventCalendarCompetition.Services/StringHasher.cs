using System;
using System.Security.Cryptography;
using System.Text;

namespace DevAdventCalendarCompetition.Services
{

    //solution basen on http://www.obviex.com/samples/hash.aspx
    public class StringHasher
    {
        public StringHasher(HashParameters hashParameters)
        {
            _hashParameters = hashParameters;
        }

        private readonly HashParameters _hashParameters;

        public bool VerifyHash(string text, string hashValue)
        {
            // Convert base64-encoded hash value into a byte array.
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            // We must know size of hash (without salt).
            int hashSizeInBits = 256;

            // Convert size of hash from bits to bytes.
            int hashSizeInBytes = hashSizeInBits / 8;

            // Make sure that the specified hash value is long enough.
            if (hashWithSaltBytes.Length < hashSizeInBytes)
            {
                return false;
            }

            // Compute a new hash string.
            string expectedHashString = ComputeHash(text);

            // If the computed hash matches the specified hash,
            // the plain text value must be correct.
            return (hashValue == expectedHashString);
        }

        public string ComputeHash(string text)
        {
            var saltBytes = _hashParameters.Salt;

            // Convert plain text into a byte array.
            byte[] textBytes = Encoding.UTF8.GetBytes(text);

            // Allocate array, which will hold plain text and salt.
            byte[] textWithSaltBytes = new byte[textBytes.Length + saltBytes.Length];

            // Copy plain text bytes into resulting array.
            for (int i = 0; i < textBytes.Length; i++)
            {
                textWithSaltBytes[i] = textBytes[i];
            }

            // Append salt bytes to the resulting array.
            for (int i = 0; i < saltBytes.Length; i++)
            {
                textWithSaltBytes[textBytes.Length + i] = saltBytes[i];
            }

            // Initialize hashing algorithm class.
            HashAlgorithm hash = new SHA256Managed();

            // Compute hash value of our text with appended salt.
            byte[] hashBytes = textWithSaltBytes;
            for (int i = 0; i < _hashParameters.Iterations; i++)
            {
                hashBytes = hash.ComputeHash(hashBytes);
            }

            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(hashBytes);

            // Return the result.
            return hashValue;
        }
    }
}