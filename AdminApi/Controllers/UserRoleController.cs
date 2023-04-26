using System;
using Common.Admin.Dto;
using Microsoft.AspNetCore.Mvc;
using ViewModels;
using AutoMapper;
using Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace AdminApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("admin/user-role")]
    public class UserRoleController
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
        public bool AssignRole(UserInfoRoleViewModel userRoleViewModel)
        {
            var userRoleDto = _mapper.Map<UserInfoRoleDto>(userRoleViewModel);
            var result = _userRoleService.AssignRole(userRoleDto);

            return result;
        }

        [HttpPost("deassign")]
        public bool DeassignRole(UserInfoRoleViewModel userRoleViewModel)
        {
            var userRoleDto = _mapper.Map<UserInfoRoleDto>(userRoleViewModel);
            var result = _userRoleService.DeassignRole(userRoleDto);

            return result;
        }
    }
}

