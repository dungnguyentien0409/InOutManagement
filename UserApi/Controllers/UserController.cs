using System;
using UserApi.Interfaces;
using Requests;
using AutoMapper;
using Common.UserDto;
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
		public IActionResult SignUp(SignupRequest viewModel)
		{
			var userDto = _mapper.Map<UserInfoDto>(viewModel);

			if (!_userServices.SignUp(userDto))
			{
				return BadRequest();
			}

			return Ok(true);
		}

        [HttpPost("signin")]
        public IActionResult SignIn(SigninRequest viewModel)
        {
            var userDto = _mapper.Map<UserInfoDto>(viewModel);
			var jwtToken = _userServices.SignIn(userDto);

			if (string.IsNullOrEmpty(jwtToken))
			{
				return BadRequest();
			}

            return Ok(jwtToken);
        }
    }
}

