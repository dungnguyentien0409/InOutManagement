using System;
using Common.DoorDto;
using ViewModels;
using Microsoft.AspNetCore.Mvc;
using Common.AdminDto;
using AutoMapper;
using Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace AdminApi.Controllers
{
    [Authorize(Roles="Admin")]
    [ApiController]
    [Route("admin/door-role")]
    public class DoorRoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ILogger<DoorRoleController> _logger;
        private IDoorRoleService _doorRoleService;

		public DoorRoleController(IMapper mapper, ILogger<DoorRoleController> logger, IDoorRoleService doorRoleService)
		{
            _mapper = mapper;
            _logger = logger;
            _doorRoleService = doorRoleService;
		}

        [HttpPost("assign")]
        public bool AssignRole(DoorRoleViewModel viewModel)
        {
            var dto = _mapper.Map<DoorRoleDto>(viewModel);

            return _doorRoleService.AssignDoorRole(dto);
        }

        [HttpPost("deassign")]
        public bool DeassignRole(DoorRoleViewModel viewModel)
        {
            var dto = _mapper.Map<DoorRoleDto>(viewModel);

            return _doorRoleService.DeassignDoorRole(dto);
        }
    }
}

