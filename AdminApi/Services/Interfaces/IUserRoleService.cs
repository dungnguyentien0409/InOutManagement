using System;
using Common.AdminDto;

namespace AdminApi.Interfaces
{
	public interface IUserRoleService
	{
        bool AssignRole(UserInfoRoleDto userInfoRoleDto);
        bool DeassignRole(UserInfoRoleDto userInfoRoleDto);
    }
}

