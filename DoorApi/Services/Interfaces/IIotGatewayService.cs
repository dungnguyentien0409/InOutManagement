using System;
using Common.DoorDto;

namespace DoorApi.Interfaces
{
	public interface IIotGatewayService
    {
		public Task SendDoorStatus(TapDoorDto tapDoorDto);
	}
}

