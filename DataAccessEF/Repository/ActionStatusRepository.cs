using System;
using Entities;
using Interfaces;

namespace DataAccessEF.Repository
{
	public class ActionStatusRepository : GenericRepository<ActionStatus, InOutManagementContext>, IActionStatusRepository
    {
		public ActionStatusRepository(InOutManagementContext context) : base(context)
		{
		}
	}
}

