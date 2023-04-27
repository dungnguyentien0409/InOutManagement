using System;
namespace Common.AdminDto
{
	public class DoorRoleDto
	{
		public Guid Id { get; set; }
		public Guid DoorId { get; set; }
		public Guid RoleId { get; set; }
		public string DoorName { get; set; }
		public string RoleName { get; set; }
	}
}

