using System;
using Common.DoorDto;

namespace DoorApi.Interfaces
{
	public interface IInOutHistoryService
	{
		Task SaveToHistory(TapDoorDto dto);
	}
}

