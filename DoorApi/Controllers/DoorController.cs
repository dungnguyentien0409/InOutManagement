using System;
using AutoMapper;
using ViewModels;
using Common.Door.Dto;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DoorApi.Controllers
{
    [ApiController]
    [Route("door")]
    public class DoorController : ControllerBase
    {
		private readonly IDoorService _doorService;
		private readonly IIotGatewayService _iotGatewayService;
		private readonly IMapper _mapper;

		public DoorController(IDoorService doorService, IIotGatewayService iotGatewayService, IMapper mapper)
		{
			_doorService = doorService;
            _iotGatewayService = iotGatewayService;
			_mapper = mapper;
		}

		[HttpPost("open")]
		public bool Open(OpenDoorViewModel viewModel)
		{
            var dto = _mapper.Map<DoorDto>(viewModel);

			if (!_doorService.ValidOpen(dto))
			{
				return false;
			}

			_iotGatewayService.SendDoorStatus(dto, new StatusDto { Name = "Open" });

			return true;
		}

        [HttpPost("create")]
        public bool Create(DoorViewModel viewModel)
        {
            var dto = _mapper.Map<DoorDto>(viewModel);

            return _doorService.CreateDoor(dto);
        }

		[HttpGet("test-admin")]
		public string TestAdmin()
		{
			return "Admin role";
		}

        [HttpGet("test-normal")]
        public string TestNormal()
        {
            return "Normal role";
        }
    }
}

