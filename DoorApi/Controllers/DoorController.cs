using System;
using AutoMapper;
using ViewModels;
using Common.DoorDto;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Azure;
using Common;

namespace DoorApi.Controllers
{
    [ApiController]
    [Route("door")]
    public class DoorController : ControllerBase
    {
		private readonly IDoorService _doorService;
		private readonly IIotGatewayService _iotGatewayService;
		private readonly IInOutHistoryService _historyService;
		private readonly IMapper _mapper;
		private readonly ILogger<DoorController> _logger;

		public DoorController(IDoorService doorService, IIotGatewayService iotGatewayService, IMapper mapper,
			ILogger<DoorController> logger, IInOutHistoryService historyService)
		{
			_doorService = doorService;
            _iotGatewayService = iotGatewayService;
			_mapper = mapper;
			_logger = logger;
			_historyService = historyService;
		}

		[HttpPost("open")]
		public async Task<bool> Open(TapDoorViewModel viewModel)
		{
			try
			{
				var dto = _mapper.Map<TapDoorDto>(viewModel);

				if (!_doorService.Open(dto).Result)
				{
					_logger.LogError("open success");
					dto.TapAction = string.IsNullOrEmpty(dto.TapAction) ? "" :
						dto.TapAction == Constants.TapAction.TAPIN ? Constants.TapAction.FAILED_TAPIN :
						dto.TapAction == Constants.TapAction.TAPOUT ? Constants.TapAction.FAILED_TAPOUT :
						"";
                    _historyService.SaveToHistory(dto);

                    return false;
				}

                _logger.LogError("open fail");
                _iotGatewayService.SendDoorStatus(dto);
                _historyService.SaveToHistory(dto);

                return true;
			}
			catch(Exception ex)
			{
				_logger.LogError("Error when opening the door: " + ex.Message);

				return false;
			}
		}

        [HttpPost("create")]
        public bool Create(DoorViewModel viewModel)
        {
			try
			{
				var dto = _mapper.Map<DoorDto>(viewModel);

				return _doorService.CreateDoor(dto);
			}
			catch(Exception ex)
			{
				_logger.LogError("Error when creating new door : " + ex.Message);
				return false;
			}
        }
    }
}

