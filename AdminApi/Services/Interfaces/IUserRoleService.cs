using System;
using Common.Admin.Dto;

namespace Interfaces
{
	public interface IUserRoleService
	{
        bool AssignRole(UserInfoRoleDto userInfoRoleDto);
        bool DeassignRole(UserInfoRoleDto userInfoRoleDto);
    }
}

