using System;
using Entities;
using Interfaces;

namespace DataAccessEF.Repository
{
	public class DoorRoleRepository : GenericRepository<DoorRole, InOutManagementContext>, IDoorRoleRepository
    {
		public DoorRoleRepository(InOutManagementContext context) : base(context)
        {
		}
	}
}

