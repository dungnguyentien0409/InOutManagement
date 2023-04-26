﻿using System;
using Interfaces;
using Entities;
using Common.User.Dto;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Implementations
{
	public class UserService : IUserService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
		private readonly IConfiguration _config;
        private readonly ILogger<UserService> _logger;
		private readonly int SALT_SIZE;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config,
			ILogger<UserService> logger)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_config = config;
			_logger = logger;
			SALT_SIZE = _config.GetValue<int>("SaltSize");
        }

		public bool SignUp(UserInfoDto userDto)
		{
			if (!VerifyUserInfo(userDto))
			{
				return false;
			}

			var userInfo = new UserInfo();
			userInfo = _mapper.Map<UserInfo>(userDto);
			userInfo.Id = Guid.NewGuid();
			userInfo.Salt = Common.Helpers.PasswordHelper.GenerateSalt(SALT_SIZE);
			userInfo.HashedPassword = Common.Helpers.PasswordHelper.HashPassword(userDto.Password, userInfo.Salt);

			_unitOfWork.UserInfo.Add(userInfo);

			_unitOfWork.Save();

			return true;
		}

		public string SignIn(UserInfoDto userDto)
		{
			var userItem = _unitOfWork.UserInfo.Find(f => f.UserName == userDto.UserName).FirstOrDefault();

			if (userItem == null
				|| !Common.Helpers.PasswordHelper.VerifyPassword(userDto.Password, userItem.Salt, userItem.HashedPassword))
			{
				return "";
			}

            var userRoleItem = _unitOfWork.UserInfoRole.Find(f => f.UserInfoId == userItem.Id).FirstOrDefault();

			if (userRoleItem == null)
			{
				_logger.LogError("This user does not have any role");
				return "";
			}

			userDto.UserRole = userRoleItem.Role.Name;

            return GenerateJwtToken(userDto);
		}

		private bool VerifyUserInfo(UserInfoDto userDto)
		{
			if (string.IsNullOrEmpty(userDto.UserName) || string.IsNullOrEmpty(userDto.Password))
			{
				return false;
			}

			var userItem = _unitOfWork.UserInfo.Find(f => f.UserName == userDto.UserName).FirstOrDefault();

			if (userItem != null)
			{
				return false;
			}

			return true;
		}

		private string GenerateJwtToken(UserInfoDto userDto)
		{
			var secretKey = _config.GetValue<string>("Jwt:SecretKey");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userDto.UserName),
				new Claim(ClaimTypes.Role, userDto.UserRole),
				new Claim("Email", userDto.Email)
            };
			var issuer = _config.GetValue<string>("Jwt:Issuer");
			var audience = _config.GetValue<string>("Jwt:Audience");
			var expire = _config.GetValue<int>("Jwt:ExpireInMinutes");

            var token = new JwtSecurityToken(issuer,
                audience,
                claims,
                expires: DateTime.Now.AddMinutes(expire),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateCurrentToken(string token)
        {
            var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = _config.GetValue<string>("Issuer");
            var myAudience = _config.GetValue<string>("Audience");

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}