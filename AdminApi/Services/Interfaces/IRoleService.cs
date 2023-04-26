using System;
using Common.Admin.Dto;

namespace Interfaces
{
	public interface IRoleService
	{
		bool CreateRole(RoleDto roleDto);
    }
}

