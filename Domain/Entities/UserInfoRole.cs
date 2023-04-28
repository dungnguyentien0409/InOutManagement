using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	[Table("UserRole")]
	public class UserInfoRole : EntityBase
	{
		[ForeignKey("UserInfo")]
		public Guid UserInfoId { get; set; }
		public virtual UserInfo UserInfo { get; set; }

		[ForeignKey("Role")]
		public Guid RoleId { get; set; }
		public virtual Role Role { get; set; }
	}
}

