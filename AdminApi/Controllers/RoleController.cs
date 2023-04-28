using System;
using Interfaces;
using Requests;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Common.UserDto;
using Common.AdminDto;
using Microsoft.AspNetCore.Authorization;

namespace UserApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("admin/role")]
    public class RoleController : ControllerBase
    {
		IRoleService _roleService;
		IMapper _mapper;
		
		public RoleController(IRoleService roleService, IMapper mapper)
		{
			_roleService = roleService;
			_mapper = mapper;
		}

        [HttpPost("create")]
        public bool CreateRole(RoleRequest roleViewModel)
		{
			var roleDto = _mapper.Map<RoleDto>(roleViewModel);
			var result = _roleService.CreateRole(roleDto);

			return result;
		}
    }
}

