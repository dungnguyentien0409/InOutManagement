using System;
using Common.Admin.Dto;

namespace Interfaces
{
	public interface IDoorRoleService
	{
        public bool AssignDoorRole(DoorRoleDto doorRoleDto);
        public bool DeassignDoorRole(DoorRoleDto doorRoleDto);
    }
}

