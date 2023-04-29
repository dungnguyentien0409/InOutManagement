using System;
using Entities;
using Domain.Interfaces;

namespace DataAccessEF.Repository
{
	public class UserInfoRepository : GenericRepository<UserInfo, InOutManagementContext>, IUserInfoRepository
    {
		public UserInfoRepository(InOutManagementContext context) : base(context)
        {
		}
	}
}

