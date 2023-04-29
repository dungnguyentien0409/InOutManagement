using System;
using Common.AdminDto;

namespace AdminApi.Interfaces
{
	public interface IDoorRoleService
	{
        public bool AssignDoorRole(DoorRoleDto doorRoleDto);
        public bool DeassignDoorRole(DoorRoleDto doorRoleDto);
    }
}

