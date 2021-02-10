using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalSupport.Utils
{
    public class PasswordHasher
    {
        private readonly string _globalSalt;
        private const int localSaltLength = 15;
        public PasswordHasher()
        {
            //TODO : get globalsalt from appsettings

            _globalSalt = "qwerty";
        }

        /// <summary>
        /// Method to create fixed-length string 
        /// </summary>
        /// <returns>Returns fixed-length string of arbitraty characters</returns>
        public string GetLocalSalt()
        {

            int length = localSaltLength;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rnd = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(x => x[rnd.Next(length)]).ToArray());
        }

        public string GetPasswordHash(string password , string localSalt)
        {
            return Hash(Hash(password + localSalt) + _globalSalt);
        }


        private string Hash(string line)
        {
            using var hashEncoder = SHA256Managed.Create();

            StringBuilder sbuilder = new StringBuilder();
            Encoding stringEncoder = Encoding.UTF8;

            byte[] hash = hashEncoder.ComputeHash(stringEncoder.GetBytes(line));

            foreach(var vbyte in hash)
            {
                sbuilder.Append(vbyte.ToString("x2"));
            }


            return sbuilder.ToString();
        }
    }
}
