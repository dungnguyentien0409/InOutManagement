using System;
using Interfaces;
using ViewModels;
using AutoMapper;
using Common.User.Dto;
using Microsoft.AspNetCore.Mvc;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
		IUserService _userServices;
		IMapper _mapper;

		public UserController(IUserService userService, IMapper mapper)
		{
			_userServices = userService;
			_mapper = mapper;
		}

		[HttpPost("signup")]
		public bool SignUp(UserViewModel viewModel)
		{
			var userDto = _mapper.Map<UserInfoDto>(viewModel);

			if (!_userServices.SignUp(userDto))
			{
				return false;
			}

			return true;
		}

        [HttpPost("signin")]
        public string SignIn(UserViewModel viewModel)
        {
            var userDto = _mapper.Map<UserInfoDto>(viewModel);
			var jwtToken = _userServices.SignIn(userDto);

            return jwtToken;
        }
    }
}

