using System;
namespace Requests
{
	public class DoorRoleRequest
	{
		public Guid? Id { get; set; }
		public Guid? DoorId { get; set; }
		public Guid? Role { get; set; }
		public string DoorName { get; set; }
		public string RoleName { get; set; }
	}
}

