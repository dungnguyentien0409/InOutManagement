using System;
using Entities;
using Interfaces;

namespace DataAccessEF.Repository
{
	public class InOutHistoryRepository : GenericRepository<InOutHistory, InOutManagementContext>, IInOutHistoryRepository
    {
		public InOutHistoryRepository(InOutManagementContext context) : base(context)
		{
		}
	}
}

