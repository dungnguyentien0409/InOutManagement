using System;
using Common.AdminDto;

namespace Interfaces
{
	public interface IRoleService
	{
		bool CreateRole(RoleDto roleDto);
    }
}

