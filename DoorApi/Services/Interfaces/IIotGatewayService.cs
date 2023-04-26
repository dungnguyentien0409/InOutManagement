using System;
using Common.Door.Dto;

namespace Interfaces
{
	public interface IIotGatewayService
    {
		public void SendDoorStatus(DoorDto doorDto, StatusDto statusDto);
	}
}

