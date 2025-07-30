using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Web;

namespace QueueAPI.Helpers
{
    public class CryptoHelper
    {
        public string HashSHA256(string password, string salt)
        {
            byte[] valueBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            byte[] saltedValue = valueBytes.Concat(saltBytes).ToArray();

            using (var sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(saltedValue);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        private string GenerateRandomPassword()
        {
            string password = "";

            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[32];
                rng.GetBytes(tokenData);

                password = Convert.ToBase64String(tokenData);
            }

            return password;
        }
    }
}