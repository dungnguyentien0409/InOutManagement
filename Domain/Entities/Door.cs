using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	[Table("Door")]
	public class Door : EntityBase
	{
		[MaxLength(100)]
		public string Name { get; set; }
		public string Description { get; set; }
	}
}

