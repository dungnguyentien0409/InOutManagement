﻿using System;
namespace Common.AdminDto
{
	public class UserInfoRoleDto
	{
		public Guid Id { get; set; }
		public Guid UserInfoId { get; set; }
		public Guid RoleId { get; set; }
		public string RoleName { get; set; }
		public string UserName { get; set; }
	}
}

