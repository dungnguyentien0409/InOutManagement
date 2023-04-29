using System;
using Entities;
using Domain.Interfaces;

namespace DataAccessEF.Repository
{
	public class RoleRepository : GenericRepository<Role, InOutManagementContext>, IRoleRepository
    {
		public RoleRepository(InOutManagementContext context) : base(context)
        {
		}
	}
}

