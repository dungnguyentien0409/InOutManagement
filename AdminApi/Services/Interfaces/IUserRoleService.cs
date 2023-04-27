using System;
using Common.AdminDto;

namespace Interfaces
{
	public interface IUserRoleService
	{
        bool AssignRole(UserInfoRoleDto userInfoRoleDto);
        bool DeassignRole(UserInfoRoleDto userInfoRoleDto);
    }
}

