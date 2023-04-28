using System;
namespace Requests
{
	public class UserInfoRoleRequest
	{
		public Guid? Id { get; set; }
		public Guid? UserInfoId { get; set; }
		public Guid? RoleId { get; set; }
		public string UserName { get; set; }
		public string RoleName { get; set; }
	}
}

