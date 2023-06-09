﻿using System;
using DataAccessEF;
using DataAccessEF.Repository;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessEF.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private InOutManagementContext context;

		public IDoorRepository Door { get; }
		public IDoorRoleRepository DoorRole { get; }
		public IRoleRepository Role { get; }
		public IUserInfoRepository UserInfo { get; }
		public IUserInfoRoleRepository UserInfoRole { get; }
		public IActionStatusRepository ActionStatus { get; }

		public UnitOfWork(InOutManagementContext context)
		{
			this.context = context;

			Door = new DoorRepository(context);
			DoorRole = new DoorRoleRepository(context);
			Role = new RoleRepository(context);
			UserInfo = new UserInfoRepository(context);
			UserInfoRole = new UserInfoRoleRepository(context);
			ActionStatus = new ActionStatusRepository(context);
		}

        public void Dispose()
        {
            context.Dispose();
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}

