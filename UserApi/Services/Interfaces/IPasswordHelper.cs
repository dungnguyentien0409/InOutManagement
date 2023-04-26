using System;
namespace Interfaces
{
	public interface IPasswordHelper
	{
		string HashPassword(string password, string salt);
		string GenerateSalt();
		bool VerifyPassword(string passWord, string salt, string hashedPassword);

    }
}

