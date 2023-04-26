using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	[Table("ActionStatus")]
	public class ActionStatus : EntityBase
	{
		[MaxLength(100)]
		public string Name { get; set; }
	}
}

