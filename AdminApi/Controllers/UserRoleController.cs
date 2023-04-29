using System;
using Common.AdminDto;
using Microsoft.AspNetCore.Mvc;
using Requests;
using AutoMapper;
using AdminApi.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace AdminApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("admin/user-role")]
    public class UserRoleController : ControllerBase
    {
        private readonly ILogger<UserRoleController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRoleService _userRoleService;

		public UserRoleController(ILogger<UserRoleController> logger, IMapper mapper, IUserRoleService userRoleService)
		{
            _logger = logger;
            _mapper = mapper;
            _userRoleService = userRoleService;
		}

        [HttpPost("assign")]
        public bool AssignRole(UserInfoRoleRequest userRoleViewModel)
        {
            var userRoleDto = _mapper.Map<UserInfoRoleDto>(userRoleViewModel);
            var result = _userRoleService.AssignRole(userRoleDto);

            return result;
        }

        [HttpPost("deassign")]
        public bool DeassignRole(UserInfoRoleRequest userRoleViewModel)
        {
            var userRoleDto = _mapper.Map<UserInfoRoleDto>(userRoleViewModel);
            var result = _userRoleService.DeassignRole(userRoleDto);

            return result;
        }
    }
}

