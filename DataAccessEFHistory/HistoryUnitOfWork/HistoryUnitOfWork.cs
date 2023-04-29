using System;
using DataAccessEF;
using DataAccessEF.Repository;
using DomainHistory.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessEF.UnitOfWork
{
	public class HistoryUnitOfWork : IHistoryUnitOfWork
	{
		private HistoryContext context;

		public IInOutHistoryRepository InOutHistory { get; }

		public HistoryUnitOfWork(HistoryContext context)
		{
			this.context = context;

			InOutHistory = new InOutHistoryRepository(context);
		}

        public void Dispose()
        {
            context.Dispose();
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}

