﻿using System;
namespace Common.UserDto
{
	public class UserInfoDto
	{
		public Guid Id { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string UserRole { get; set; }
	}
}

