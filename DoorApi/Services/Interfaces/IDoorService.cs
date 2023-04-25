using System;
using Dto;

namespace Interfaces
{
	public interface IDoorService
	{
		public bool ValidOpen(DoorDto doorDto);
	}
}

