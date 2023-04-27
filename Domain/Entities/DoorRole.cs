using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	[Table("DoorRole")]
	public class DoorRole : EntityBase
	{
		[ForeignKey("Door")]
		public Guid DoorId { get; set; }
		public virtual Door? Door { get; set; }

		[ForeignKey("Role")]
		public Guid RoleId { get; set; }
		public virtual Role? Role { get; set; }
	}
}

