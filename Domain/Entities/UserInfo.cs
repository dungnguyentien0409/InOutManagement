using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	[Table("UserInfo")]
	public class UserInfo : EntityBase
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Salt { get; set; }
		public string HashedPassword { get; set; }
	}
}

