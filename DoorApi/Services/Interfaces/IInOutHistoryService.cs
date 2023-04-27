using System;
using Common.DoorDto;

namespace Interfaces
{
	public interface IInOutHistoryService
	{
		Task SaveToHistory(TapDoorDto dto);
	}
}

