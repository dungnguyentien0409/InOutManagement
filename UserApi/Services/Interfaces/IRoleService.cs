using System;
using Dto;

namespace Interfaces
{
	public interface IRoleService
	{
		bool CreateRole(RoleDto roleDto);
		bool AssignRole(UserInfoRoleDto userInfoRoleDto);
		bool DeassignRole(UserInfoRoleDto userInfoRoleDto);
    }
}

