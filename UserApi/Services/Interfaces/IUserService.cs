using System;
using Common.UserDto;

namespace Interfaces
{
	public interface IUserService
	{
		bool SignUp(UserInfoDto userDto);
		string SignIn(UserInfoDto userDto);
	}
}

