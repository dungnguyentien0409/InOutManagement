using System;
using Interfaces;
using Common.Door.Dto;

namespace Implementations
{
	public class IotGatewayService : IIotGatewayService
    {
		public void SendDoorStatus(DoorDto doorDto, StatusDto statusDto)
		{
			// Send to iot gateway endpoint
			Console.Write(doorDto.Name + "\t" + statusDto.Name);
		}
    }
}

