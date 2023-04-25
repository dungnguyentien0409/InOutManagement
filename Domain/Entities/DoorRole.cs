using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	[Table("DoorRole")]
	public class DoorRole : EntityBase
	{
		[ForeignKey("Door")]
		public Guid DoorId { get; set; }
		public Door Door { get; set; }

		[ForeignKey("Role")]
		public Guid RoleId { get; set; }
		public Role Role { get; set; }
	}
}

