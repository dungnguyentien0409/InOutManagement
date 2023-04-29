using System;
using Common.DoorDto;

namespace DoorApi.Interfaces
{
	public interface IDoorService
	{
		public Task<bool> Open(TapDoorDto doorDto);
		public bool CreateDoor(DoorDto doorDto);
		
	}
}

