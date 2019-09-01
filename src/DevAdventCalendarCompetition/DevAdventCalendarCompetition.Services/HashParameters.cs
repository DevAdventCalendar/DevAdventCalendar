using System;
using System.Security.Cryptography;
using System.Text;

namespace DevAdventCalendarCompetition.Services
{
    public class HashParameters
    {
        public HashParameters(int iterations, byte salt)
        {
            this.Iterations = iterations;
            this.Salt = salt;
        }

        public int Iterations { get; set; }

        public byte Salt { get; set; }
    }
}