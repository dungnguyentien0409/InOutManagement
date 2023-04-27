using System;
using Common.AdminDto;

namespace Interfaces
{
	public interface IDoorRoleService
	{
        public bool AssignDoorRole(DoorRoleDto doorRoleDto);
        public bool DeassignDoorRole(DoorRoleDto doorRoleDto);
    }
}

