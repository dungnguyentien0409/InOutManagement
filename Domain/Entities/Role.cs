using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	[Table("Role")]
	public class Role : EntityBase
	{
		public string Name { get; set; }
	}
}

