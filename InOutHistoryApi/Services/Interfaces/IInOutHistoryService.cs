using System;
using Common.InOutHistoryDto;
using Request;

namespace Interfaces
{
	public interface IInOutHistoryService
	{
		Task<List<InOutHistoryDto>> GetInOutHistories(InOutHistoryRequest request);
		Task AddHistory(InOutHistoryRequest request);
	}
}

