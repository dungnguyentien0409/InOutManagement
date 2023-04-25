using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	[Table("UserRole")]
	public class UserInfoRole : EntityBase
	{
		[ForeignKey("UserInfo")]
		public Guid UserInfoId { get; set; }
		public UserInfo UserInfo { get; set; }

		[ForeignKey("Role")]
		public Guid RoleId { get; set; }
		public Role Role { get; set; }
	}
}

