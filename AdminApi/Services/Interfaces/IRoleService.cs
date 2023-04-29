using System;
using Common.AdminDto;

namespace AdminApi.Interfaces
{
	public interface IRoleService
	{
		bool CreateRole(RoleDto roleDto);
    }
}

