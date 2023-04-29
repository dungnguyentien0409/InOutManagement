using System;
using Entities;
using Domain.Interfaces;

namespace DataAccessEF.Repository
{
	public class DoorRoleRepository : GenericRepository<DoorRole, InOutManagementContext>, IDoorRoleRepository
    {
		public DoorRoleRepository(InOutManagementContext context) : base(context)
        {
		}
	}
}

