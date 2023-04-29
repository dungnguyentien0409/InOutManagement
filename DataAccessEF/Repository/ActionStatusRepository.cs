using System;
using Entities;
using Domain.Interfaces;

namespace DataAccessEF.Repository
{
	public class ActionStatusRepository : GenericRepository<ActionStatus, InOutManagementContext>, IActionStatusRepository
    {
		public ActionStatusRepository(InOutManagementContext context) : base(context)
		{
		}
	}
}

