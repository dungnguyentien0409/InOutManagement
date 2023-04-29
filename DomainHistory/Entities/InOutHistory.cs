using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainHistory.Entities
{
	[Table("InOutHistory")]
	public class InOutHistory : EntityBase
	{
		public Guid? UserId { get; set; }
		public string? UserName { get; set; }

		public Guid? DoorId { get; set; }
		public string? DoorName { get; set; }

        public Guid? ActionStatusId { get; set; }
		public string? ActionStatusName { get; set; }
	}
}

