using System;
using Interfaces;
using ViewModels;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Common.User.Dto;
using Common.Admin.Dto;
using Microsoft.AspNetCore.Authorization;

namespace UserApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("admin/role")]
    public class RoleController
	{
		IRoleService _roleService;
		IMapper _mapper;
		
		public RoleController(IRoleService roleService, IMapper mapper)
		{
			_roleService = roleService;
			_mapper = mapper;
		}

        [HttpPost("create")]
        public bool CreateRole(RoleViewModel roleViewModel)
		{
			var roleDto = _mapper.Map<RoleDto>(roleViewModel);
			var result = _roleService.CreateRole(roleDto);

			return result;
		}
    }
}

