using System;
using Common.User.Dto;

namespace Interfaces
{
	public interface IUserService
	{
		bool SignUp(UserInfoDto userDto);
		string SignIn(UserInfoDto userDto);
	}
}

