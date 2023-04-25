using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	[Table("Door")]
	public class Door : EntityBase
	{
		public string Name { get; set; }
	}
}

