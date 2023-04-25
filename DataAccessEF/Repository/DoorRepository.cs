using System;
using System.Reflection.PortableExecutable;
using Entities;
using Interfaces;

namespace DataAccessEF.Repository
{
	public class DoorRepository : GenericRepository<Door, InOutManagementContext>, IDoorRepository
    {
		public DoorRepository(InOutManagementContext context) : base(context)
		{
		}
	}
}

