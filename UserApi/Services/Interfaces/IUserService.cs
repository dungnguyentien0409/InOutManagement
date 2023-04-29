using System;
using Common.UserDto;

namespace UserApi.Interfaces
{
	public interface IUserService
	{
		bool SignUp(UserInfoDto userDto);
		string SignIn(UserInfoDto userDto);
	}
}

