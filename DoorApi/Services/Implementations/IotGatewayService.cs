using System;
using Interfaces;
using Common.DoorDto;

namespace Implementations
{
	public class IotGatewayService : IIotGatewayService
    {
		public async Task SendDoorStatus(TapDoorDto dto)
		{
			// Send to iot gateway endpoint
			Console.Write(dto.UserName + "\t" + dto.DoorName);
		}
    }
}

