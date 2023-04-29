using System;
using DomainHistory.Interfaces;

namespace DomainHistory.Interfaces
{
	public interface IHistoryUnitOfWork : IDisposable
	{
		public IInOutHistoryRepository InOutHistory { get; }

        int Save();
    }
}

