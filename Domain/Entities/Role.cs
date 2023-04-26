using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	[Table("Role")]
	public class Role : EntityBase
	{
		[MaxLength(100)]
		public string Name { get; set; }
	}
}

