﻿using System;
namespace Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		public IDoorRepository Door { get; }
		public IDoorRoleRepository DoorRole { get; }
		public IRoleRepository Role { get; }
		public IUserInfoRepository UserInfo { get; }
		public IUserInfoRoleRepository UserInfoRole { get; }
		public IInOutHistoryRepository InOutHistory { get; }
		public IActionStatusRepository ActionStatus { get; }

        int Save();
    }
}

