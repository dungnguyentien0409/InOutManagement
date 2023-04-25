using System;
using Entities;
using Interfaces;

namespace DataAccessEF.Repository
{
	public class UserInfoRoleRepository : GenericRepository<UserInfoRole, InOutManagementContext>, IUserInfoRoleRepository
    {
		public UserInfoRoleRepository(InOutManagementContext context) : base(context)
        {
		}
	}
}

