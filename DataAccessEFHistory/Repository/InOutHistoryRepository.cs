using System;
using DomainHistory.Entities;
using DomainHistory.Interfaces;

namespace DataAccessEF.Repository
{       
	public class InOutHistoryRepository : GenericRepository<InOutHistory, HistoryContext>, IInOutHistoryRepository
    {
		public InOutHistoryRepository(HistoryContext context) : base(context)
		{
		}
	}
}

