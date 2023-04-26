using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	[Table("UserInfo")]
    public class UserInfo : EntityBase
	{
		[MaxLength(100)]
        public string UserName { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

		public string Salt { get; set; }

		public string HashedPassword { get; set; }
	}
}

