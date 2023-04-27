using System;
using Common.DoorDto;

namespace Interfaces
{
	public interface IIotGatewayService
    {
		public Task SendDoorStatus(TapDoorDto tapDoorDto);
	}
}

