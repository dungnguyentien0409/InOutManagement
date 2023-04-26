using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Helpers
{
	public static class PasswordHelper
	{
        public static string HashPassword(string passWord, string salt)
        {
            var algorithm = new SHA256Managed();

            var plainTextWithSaltBytes = Encoding.ASCII.GetBytes(passWord + salt);
            var hashedResult = algorithm.ComputeHash(plainTextWithSaltBytes);

            return Convert.ToBase64String(hashedResult);
        }

        public static bool VerifyPassword(string passWord, string salt, string hashedPassword)
        {
            var generatedHasedPassword = HashPassword(passWord, salt);

            return generatedHasedPassword.CompareTo(hashedPassword) == 0;
        }

        public static string GenerateSalt(int saltSize)
        {
            var rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[saltSize];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }
    }
}

