using System;
using Dto;

namespace Interfaces
{
	public interface IUserService
	{
		bool SignUp(UserInfoDto userDto);
		string SignIn(UserInfoDto userDto);
	}
}

