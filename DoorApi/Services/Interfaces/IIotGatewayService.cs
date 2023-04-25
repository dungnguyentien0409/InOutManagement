using System;
using Dto;

namespace Interfaces
{
	public interface IIotGatewayService
    {
		public void SendDoorStatus(DoorDto doorDto, StatusDto statusDto);
	}
}

