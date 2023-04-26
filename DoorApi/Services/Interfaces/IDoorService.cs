using System;
using Common.Door.Dto;

namespace Interfaces
{
	public interface IDoorService
	{
		public bool ValidOpen(DoorDto doorDto);
		public bool CreateDoor(DoorDto doorDto);
		
	}
}

