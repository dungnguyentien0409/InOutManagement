using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using Interfaces;
namespace Implementations
{
	public class PasswordHelper : IPasswordHelper
	{
        private IConfiguration _config;

        public PasswordHelper(IConfiguration config)
		{
            _config = config;
		}

        public string HashPassword(string passWord, string salt)
        {
            var algorithm = new SHA256Managed();

            var plainTextWithSaltBytes = Encoding.ASCII.GetBytes(passWord + salt);
            var hashedResult = algorithm.ComputeHash(plainTextWithSaltBytes);

            return Convert.ToBase64String(hashedResult);
        }

        public bool VerifyPassword(string passWord, string salt, string hashedPassword)
        {
            var generatedHasedPassword = HashPassword(passWord, salt);

            return generatedHasedPassword.CompareTo(hashedPassword) == 0;
        }

        public string GenerateSalt()
		{
            var saltSize = _config.GetValue<int>("SaltSize");
            var rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[saltSize];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }
    }
}

